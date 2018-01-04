namespace CmsSystem.Web.Models
{
    public class RightAction
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public bool IsGranted { get; set; }
    }
}