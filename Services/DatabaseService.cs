using Microsoft.Data.SqlClient;
using VMS.API.Models;
using System.Data;

namespace VMS.API.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ========== USERS ==========

        public async Task<List<Users>> GetUsersAsync()
        {
            var users = new List<Users>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Users", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new Users
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password"))
                        });
                    }
                }
            }

            return users;
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Users WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Users
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task<int> AddUserAsync(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Users (name, email, password) VALUES (@name, @email, @password); SELECT CAST(SCOPE_IDENTITY() as int)",
                    connection);

                command.Parameters.AddWithValue("@name", user.name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@email", user.email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@password", user.password ?? (object)DBNull.Value);

                var newId = (int)await command.ExecuteScalarAsync();
                return newId;
            }
        }

        // ========== VISITORS ==========

        public async Task<List<Visitors>> GetVisitorsAsync()
        {
            var visitors = new List<Visitors>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Visitor", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        visitors.Add(new Visitors
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password"))
                        });
                    }
                }
            }

            return visitors;
        }

        public async Task<Visitors> GetVisitorByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Visitor WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Visitors
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                            password = reader.IsDBNull(reader.GetOrdinal("password")) ? null : reader.GetString(reader.GetOrdinal("password"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task<int> AddVisitorAsync(Visitors visitor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Visitor (name, email, password) VALUES (@name, @email, @password); SELECT CAST(SCOPE_IDENTITY() as int)",
                    connection);

                command.Parameters.AddWithValue("@name", visitor.name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@email", visitor.email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@password", visitor.password ?? (object)DBNull.Value);

                var newId = (int)await command.ExecuteScalarAsync();
                return newId;
            }
        }

        // ========== DASHBOARD ==========

        public async Task<List<Dashboard>> GetDashboardDataAsync()
        {
            var dashboards = new List<Dashboard>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Dashboard", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dashboards.Add(new Dashboard
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            Purpose1 = reader.IsDBNull(reader.GetOrdinal("Purpose1")) ? null : reader.GetString(reader.GetOrdinal("Purpose1")),
                            Purpose2 = reader.IsDBNull(reader.GetOrdinal("Purpose2")) ? null : reader.GetString(reader.GetOrdinal("Purpose2")),
                            Purpose3 = reader.IsDBNull(reader.GetOrdinal("Purpose3")) ? null : reader.GetString(reader.GetOrdinal("Purpose3")),
                            Purpose4 = reader.IsDBNull(reader.GetOrdinal("Purpose4")) ? null : reader.GetString(reader.GetOrdinal("Purpose4")),
                            Purpose5 = reader.IsDBNull(reader.GetOrdinal("Purpose5")) ? null : reader.GetString(reader.GetOrdinal("Purpose5")),
                            Purpose6 = reader.IsDBNull(reader.GetOrdinal("Purpose6")) ? null : reader.GetString(reader.GetOrdinal("Purpose6")),
                            Abandon = reader.IsDBNull(reader.GetOrdinal("Abandon")) ? null : reader.GetString(reader.GetOrdinal("Abandon"))
                        });
                    }
                }
            }

            return dashboards;
        }

        // ========== REPORTS ==========

        public async Task<List<Reports>> GetReportsAsync()
        {
            var reports = new List<Reports>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Report ORDER BY Date DESC", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reports.Add(new Reports
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Purpose_1 = reader.IsDBNull(reader.GetOrdinal("Purpose_1")) ? null : reader.GetString(reader.GetOrdinal("Purpose_1")),
                            Purpose_2 = reader.IsDBNull(reader.GetOrdinal("Purpose_2")) ? null : reader.GetString(reader.GetOrdinal("Purpose_2")),
                            Purpose_3 = reader.IsDBNull(reader.GetOrdinal("Purpose_3")) ? null : reader.GetString(reader.GetOrdinal("Purpose_3")),
                            Purpose_4 = reader.IsDBNull(reader.GetOrdinal("Purpose_4")) ? null : reader.GetString(reader.GetOrdinal("Purpose_4")),
                            Purpose_5 = reader.IsDBNull(reader.GetOrdinal("Purpose_5")) ? null : reader.GetString(reader.GetOrdinal("Purpose_5")),
                            Purpose_6 = reader.IsDBNull(reader.GetOrdinal("Purpose_6")) ? null : reader.GetString(reader.GetOrdinal("Purpose_6")),
                            Purpose_7 = reader.IsDBNull(reader.GetOrdinal("Purpose_7")) ? null : reader.GetString(reader.GetOrdinal("Purpose_7")),
                            Total = reader.IsDBNull(reader.GetOrdinal("Total")) ? null : reader.GetString(reader.GetOrdinal("Total"))
                        });
                    }
                }
            }

            return reports;
        }

        public async Task<List<Reports>> GetReportsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var reports = new List<Reports>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "SELECT * FROM Report WHERE Date BETWEEN @startDate AND @endDate ORDER BY Date DESC",
                    connection);

                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        reports.Add(new Reports
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Purpose_1 = reader.IsDBNull(reader.GetOrdinal("Purpose_1")) ? null : reader.GetString(reader.GetOrdinal("Purpose_1")),
                            Purpose_2 = reader.IsDBNull(reader.GetOrdinal("Purpose_2")) ? null : reader.GetString(reader.GetOrdinal("Purpose_2")),
                            Purpose_3 = reader.IsDBNull(reader.GetOrdinal("Purpose_3")) ? null : reader.GetString(reader.GetOrdinal("Purpose_3")),
                            Purpose_4 = reader.IsDBNull(reader.GetOrdinal("Purpose_4")) ? null : reader.GetString(reader.GetOrdinal("Purpose_4")),
                            Purpose_5 = reader.IsDBNull(reader.GetOrdinal("Purpose_5")) ? null : reader.GetString(reader.GetOrdinal("Purpose_5")),
                            Purpose_6 = reader.IsDBNull(reader.GetOrdinal("Purpose_6")) ? null : reader.GetString(reader.GetOrdinal("Purpose_6")),
                            Purpose_7 = reader.IsDBNull(reader.GetOrdinal("Purpose_7")) ? null : reader.GetString(reader.GetOrdinal("Purpose_7")),
                            Total = reader.IsDBNull(reader.GetOrdinal("Total")) ? null : reader.GetString(reader.GetOrdinal("Total"))
                        });
                    }
                }
            }

            return reports;
        }
    }
}