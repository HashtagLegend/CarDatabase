using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CarWebservice.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWebservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private static string connectionString = "Server=tcp:custom3r.database.windows.net,1433;Initial Catalog=custom3rDB;Persist Security Info=False;User ID=hashtaglegend;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpGet]
        public IEnumerable<Car> GetCars()
        {
            var carList = new List<Car>();
            string sql = "select * from Cars";
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(sql, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string brand = reader.GetString(1);
                                string model = reader.GetString(2);
                                string year = reader.GetString(3);


                                var car = new Car()
                                {
                                    Id = id,
                                    Brand = brand,
                                    Model = model,
                                    Year = year

                                     
                                };

                                carList.Add(car);
                            }
                        }
                    }
                }
            }

            return carList;
        }

        // GET: api/Car/5
        [HttpGet("{inputId}", Name = "GetCarById")]
        public Car GetCarById(int inputId)
        {
            string sql = "select * from Cars " +
                         $"where Id = {inputId}";
            var carToFind = new Car();
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(sql, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string brand = reader.GetString(1);
                            string model = reader.GetString(2);
                            string year = reader.GetString(3);


                            var car = new Car()
                            {
                                Id = id,
                                Brand = brand,
                                Model = model,
                                Year = year

                            };

                            carToFind = car;
                        }

                    }
                }
            }

            return carToFind;
        }

        // POST: api/Car
        [HttpPost]
        public Car PostCar([FromBody] Car car)
        {
            string insertCar = "INSERT into Cars (Brand, Model, Year) VALUES (@Brand, @Model, @Year)";

            SqlConnection connect = new SqlConnection(connectionString);
            using (SqlCommand insertCommand = new SqlCommand(insertCar, connect))
            {
                connect.Open();
                insertCommand.Parameters.AddWithValue("@Brand", car.Brand);
                insertCommand.Parameters.AddWithValue("@Model", car.Model);
                insertCommand.Parameters.AddWithValue("@Year", car.Year);
                insertCommand.ExecuteNonQuery();
            }

            return car;

        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public Car PutCar(int id, [FromBody] Car car)
        {
            
            string updateCar = $"UPDATE Cars SET Brand = '{car.Brand}', Model = '{car.Model}', Year = '{car.Year}'  WHERE id={id};";

            SqlConnection connect = new SqlConnection(connectionString);
            using (SqlCommand insertCommand = new SqlCommand(updateCar, connect))
            {
                connect.Open();
                insertCommand.ExecuteNonQuery();
            }

            return car;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string deleteCar = $"DELETE FROM Cars WHERE Id = {id}";

            SqlConnection connect = new SqlConnection(connectionString);
            using (SqlCommand insertCommand = new SqlCommand(deleteCar, connect))
            {
                connect.Open();
                insertCommand.ExecuteNonQuery();
            }

        }




        //private static List<Car> CarList = new List<Car>()
        //{
        //    new Car("Audi", "A4", "2018"),
        //    new Car("BMW", "118", "2015")
        //};

        //// GET: api/Car
        //[HttpGet]
        //public List<Car> Get()
        //{
        //    return CarList;
        //}

        //// GET: api/Car/5
        //[HttpGet("{id}", Name = "Get")]
        //public Car Get(int id)
        //{
        //    Car car = CarList.Find(c => c.Id == id);

        //    return car;
        //}

        //// POST: api/Car
        //[HttpPost]
        //public Car Post(Car car)
        //{
        //    CarList.Add(car);
        //    return car;
        //}

        //// PUT: api/Car/5
        //[HttpPut("{id}")]
        //public Car Put(int id, [FromBody] Car carToChange)
        //{
        //    Car car = CarList.Find(c => c.Id == id);

        //    car.Brand = carToChange.Brand;
        //    car.Model = carToChange.Model;
        //    car.Year = carToChange.Year;

        //    return car;
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public Car Delete(int id)
        //{
        //    Car car = CarList.Find(c => c.Id == id);

        //    CarList.Remove(car);

        //    return car;

    }
}
