using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Core.Models.Entities;
namespace Project2.Core.Interfaces.IServices
{
    public interface IProjectService
    {
        IEnumerable<Project> GetProjects();

        Project getProjectById(int id);

        Project getProjectByStudentId(int id);

        List<Project> getListProjectByStudentId(int id);

        List<Project> getListProject(string name);

        List<Project> getListProjectById(int id, string name);

        int getCountProjectByIdTeach(int id);

        bool getNameProject(string name);

        List<Project> getProjectByIdTime(int id);
    }
}
