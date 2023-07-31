using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;
using System.Data.SqlClient;

namespace CrestASPNETCoreIdentityDemo
{
    public class CrestUserStore : IUserStore<CrestUser>, IUserPasswordStore<CrestUser>
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB; database=ASPNETCoreIdentityDemo; trusted_connection=yes;");
            connection.Open();
            return connection;
        }

        public async Task<IdentityResult> CreateAsync(CrestUser user, CancellationToken cancellationToken)
        {
            using( var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                        "insert into CrestUsers([Id],[Username],[NormalisedUsername],[PasswordHash])" +
                        "values(@id,@userName,@normalisedUserName,@passwordHash)",
                        new
                        {
                            id = user.Id,
                            userName = user.Username,
                            normalisedUserName = user.NormalisedUsername,
                            passwordHash = user.PasswordHash,
                        }
                    );
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(CrestUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<CrestUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<CrestUser>("select * from CrestUsers where Id = @id", new { id = userId });
            }
        }

        public async Task<CrestUser?> FindByNameAsync(string normalisedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<CrestUser>("select * from CrestUsers where NormalisedUsername = @name", new { name = normalisedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(CrestUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalisedUsername);
        }

        public Task<string?> GetPasswordHashAsync(CrestUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);

        }

        public Task<string> GetUserIdAsync(CrestUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);

        }

        public Task<string> GetUserNameAsync(CrestUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);

        }

        public Task<bool> HasPasswordAsync(CrestUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);

        }

        public Task SetNormalizedUserNameAsync(CrestUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalisedUsername = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(CrestUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(CrestUser user, string? userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(CrestUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
