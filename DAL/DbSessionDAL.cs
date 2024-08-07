﻿using Dapper;
using Website.DAL.Models;
using Npgsql;
using System.Reflection;
using Website.DAL;

namespace Website.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task Create(SessionModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                      values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserId)";

                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            return sessions.FirstOrDefault();
        }
        
        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
            await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });

        }

        public async Task Update(SessionModel model)
        {
            string sql = @"update DbSession
                    set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                      where DbSessionID = @DbSessionID
            ";

            await DbHelper.ExecuteAsync(sql, model);
        }
    }
}

