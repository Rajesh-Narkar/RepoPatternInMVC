using RepositoryPatternMVC.Data;
using RepositoryPatternMVC.Models;

namespace RepositoryPatternMVC.Repo
{
    public class EmpService : EmpRepo
    {
        private readonly ApplicationDbContext db;
        public EmpService(ApplicationDbContext db) {
            this.db = db;
        }

        public List<Emp> GetAllEmps()
        {
            var data=db.employees.ToList();
            return data;
        }

        public void NewEmp(Emp e)
        {
            db.employees.Add(e);
            db.SaveChanges();
        }

        public void RemoveEmp(int id)
        {
            var data=db.employees.Find(id);
            //var data=db.employees.Where(x=>x.Id == id ).SingleOrDefault();
            if (data != null)
            {
                db.employees.Remove(data);
                db.SaveChanges();
              
            }
        }

        public Emp GetEmpById(int id)
        {
            return db.employees.Find(id);
        }

        public void ModifyEmp(Emp e)
        {
            db.employees.Update(e);
            db.SaveChanges();
        }

        public List<Emp> SearchEmp(string searchTerm)
        {
            return db.employees
                .Where(e => e.Id.ToString().Contains(searchTerm) || e.Name.Contains(searchTerm) || e.Salary.ToString().Contains(searchTerm))
                .ToList();
        }
    }
}
