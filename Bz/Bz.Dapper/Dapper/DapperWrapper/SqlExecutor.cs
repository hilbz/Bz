﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bz.DapperWrapper
{
    public class SqlExecutor : IDbExecutor
    {
        private readonly IDbConnection _dbConnection;

        public SqlExecutor(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public int Execute(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Execute(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.ExecuteScalar(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.ExecuteScalar<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<dynamic> Query(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query(
                    sql,
                    param,
                    transaction,
                    buffered,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = default(int?),
            CommandType? commandType = default(CommandType?))
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query<T>(
                    sql,
                    param,
                    transaction,
                    buffered,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            object param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return _dbConnection.Query(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.ExecuteAsync(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.ExecuteScalarAsync(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.ExecuteScalarAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql,
            Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                _dbConnection.Open();
                return await _dbConnection.QueryAsync(
                    sql,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
