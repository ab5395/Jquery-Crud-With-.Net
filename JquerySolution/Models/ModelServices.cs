using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JquerySolution.Models
{
    public class ModelServices
    {
        public EmployeeDbEntities Ee = new EmployeeDbEntities();

        public List<Country> GetCountry()
        {
            return Ee.Countries.ToList();
        }

        public List<State> GetStateByCountryId(int cid)
        {
            return Ee.States.Where(x => x.CountryId == cid).ToList();
        }

        public List<City> GetCityByStateId(int sid)
        {
            return Ee.Cities.Where(x => x.StateId == sid).ToList();
        }

        public List<Employee> GetEmployee()
        {
            return Ee.Employees.ToList();
        }

        public Employee GetEmployeeDetail(int eid)
        {
            return Ee.Employees.FirstOrDefault(m => m.EmpId == eid);
        }

        public void AddEmployee(Employee emp)
        {
            Ee.Employees.Add(emp);
            Ee.SaveChanges();
        }

        public void DeleteEmployee(Employee emp)
        {
            Ee.Employees.Remove(emp);
            Ee.SaveChanges();
        }

        public int AddCountry(Country country)
        {
            Ee.Countries.Add(country);
            Ee.SaveChanges();
            int cid = country.CountryId;
            return cid;
        }

        public void UpdateEmployee(Employee emp)
        {
            Employee data = Ee.Employees.FirstOrDefault(m => m.EmpId == emp.EmpId);
            if (data != null)
            {
                data.Name = emp.Name;
                data.CityId = emp.CityId;
                data.Department = emp.Department;
                data.Mobile = emp.Mobile;
            }
            Ee.SaveChanges();
        }

        public bool DeleteEmployee(int eid)
        {
            try
            {
                Employee data = Ee.Employees.FirstOrDefault(m => m.EmpId == eid);
                Ee.Employees.Remove(data);
                Ee.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return false;
            }
        }

        public List<Image> GetImagesByEid(int eid)
        {
            return Ee.Images.Where(x => x.EmployeeId == eid).ToList();
        }

        public void AddImage(Image image)
        {
            Ee.Images.Add(image);
            Ee.SaveChanges();
        }

        public void DeleteImage(Image image)
        {
            Ee.Images.Remove(image);
            Ee.SaveChanges();
        }

        public List<Image> GetImages()
        {
            return Ee.Images.ToList();
        }
    }

}