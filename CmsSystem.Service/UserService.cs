﻿using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using System;
using System.Collections.Generic;

namespace CmsSystem.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();

        User GetByUserId(int id);

        void Add(User user);

        bool CheckExistingUser(string userName);

        bool ValidateUser(string userName, string password);

        void Update(User user);

        void Delete(int id);

        void Save();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userRepository.GetAll();
        }

        public void Add(User user)
        {
            var passwordSalt = _encryptionService.CreateSalt();
            var entity = new User
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Address = user.Address,
                Mobile = user.Mobile,
                Description = user.Description,
                Salt = passwordSalt,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Status = user.Status,
                Password = _encryptionService.EncryptPassword(user.Password, passwordSalt),
                CreatedDate = DateTime.Now
            };
            _userRepository.Add(entity);
        }

        public void Update(User user)
        {
            var userInDb = _userRepository.GetSingleById(user.Id);

            if(string.Equals(userInDb.Password, user.Password))
            {
                _userRepository.Update(user);
            }
            else
            {
                var passwordSalt = _encryptionService.CreateSalt();
                var entity = new User
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Address = user.Address,
                    Mobile = user.Mobile,
                    Description = user.Description,
                    Salt = passwordSalt,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin,
                    Status = user.Status,
                    Password = _encryptionService.EncryptPassword(user.Password, passwordSalt),
                    CreatedDate = DateTime.Now
                };
                _userRepository.Update(entity);
            }
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool CheckExistingUser(string userName)
        {
            return _userRepository.GetSingleByCondition(x => x.UserName == userName) != null;
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = _userRepository.GetSingleByCondition(x => x.UserName == userName);

            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.Password);
        }

        public User GetByUserId(int id)
        {
            return _userRepository.GetSingleById(id);
        }
    }
}