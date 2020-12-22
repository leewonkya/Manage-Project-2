using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Project2.Core.Models.Entities;

namespace Project2.Core.Services
{
    public class ProjectService : IProjectService
    {
        private IDataContext context;

        public ProjectService(IDataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Project> GetProjects()
        {
            return context.Projects.Include(x => x.Tags).ToList();
        }

        public Project getProjectById(int id)
        {
            return context.Projects.Find(id);
        }

        public List<Project> getListProject(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return context.Projects.Include(x => x.Tags).Include(x => x.GuestTeacher).Include(x => x.GuestStudent).Where(x => x.GuestTeacher.Full_name.Contains(name)).ToList();
            }
            return context.Projects.Include(x => x.Tags).Include(x => x.GuestTeacher).Include(x => x.GuestStudent).ToList();
        }

        public Project getProjectByStudentId(int id)
        {
            return context.Projects
                .Include(x => x.Tags)
                .Include(x => x.GuestTeacher)
                .Include(x => x.GuestStudent)
                .Where(x => x.GuestStudent.Id == id).SingleOrDefault();
        }

        public List<Project> getListProjectById(int id, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return context.Projects.Include(x => x.Tags).Include(x => x.GuestTeacher).Include(x => x.GuestStudent).Where(x => x.GuestTeacher.Full_name.Contains(name)).ToList();
            }
            return context.Projects.Include(x => x.Tags).Include(x => x.GuestTeacher).Include(x => x.GuestStudent).Where(x => x.GuestTeacher.Id == id).ToList();
            
        }

        public int getCountProjectByIdTeach(int id)
        {
            return context.Projects
                .Include(x => x.Tags)
                .Include(x => x.GuestTeacher)
                .Include(x => x.GuestStudent)
                .Where(x => x.GuestTeacher.Id == id).Count();
        }

        public bool getNameProject(string name)
        {
            bool check = false;

            var data = context.Projects.Where(x => x.name.Equals(name)).SingleOrDefault();

            if(data == null) // Khong tim thay project
            {
                check = true;
            }
            else // tim thay project
            {
                check = false;
            }
            return check;
        }
    }
}
