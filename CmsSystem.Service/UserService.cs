using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CmsSystem.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();

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

        }

        public void Update(User user)
        {
            _userRepository.Update(user);
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
    }
}