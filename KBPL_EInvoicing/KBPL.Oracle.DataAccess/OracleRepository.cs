using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KBPL.Oracle.DataAccess
{
    public class OracleRepository
    {
        internal IConnectionFactory _connectionFactory;
        public OracleRepository()
        {
            _connectionFactory = new ConnectionFactory();
        }
        public async virtual Task<IEnumerable<T>> GetSingleList<T>(string cmdText, Dictionary<string, string> parameters)
        {
            IEnumerable<T> result;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

            try
            {
                var con = _connectionFactory.Connection;
                {
                    result = await con.QueryAsync<T>(cmdText, dyParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                result = null;
            }

            return result;
        }

        public async virtual Task<Tuple<List<A>, List<B>>> GetMultipleList<A, B>(string cmdText, Dictionary<string, string> parameters)
        {
            //IEnumerable<T> result;
            SqlMapper.GridReader result;
            Tuple<List<A>, List<B>> tuple;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var con = _connectionFactory.Connection;
            {
                result = await con.QueryMultipleAsync(cmdText, dyParam, commandType: CommandType.StoredProcedure);
            }
            var tabs = await result.ReadAsync<A>();
            var label = await result.ReadAsync<B>();
            tuple = Tuple.Create(tabs.AsList(), label.AsList());

            return await Task.FromResult(tuple);
        }

        public async virtual Task<Tuple<List<A>, List<B>, List<C>, List<D>>> GetMultipleList<A, B, C, D>(string cmdText, Dictionary<string, string> parameters)
        {
            //IEnumerable<T> result;
            SqlMapper.GridReader result;
            Tuple<List<A>, List<B>, List<C>, List<D>> tuple;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor2", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor3", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var con = _connectionFactory.Connection;
            {
                result = await con.QueryMultipleAsync(cmdText, dyParam, commandType: CommandType.StoredProcedure);
            }
            var tabs = await result.ReadAsync<A>();
            var label = await result.ReadAsync<B>();
            var label1 = await result.ReadAsync<C>();
            var label2 = await result.ReadAsync<D>();
            tuple = Tuple.Create(tabs.AsList(), label.AsList(), label1.AsList(), label2.AsList());

            return await Task.FromResult(tuple);
        }


        public async virtual Task<Tuple<List<A>, List<B>, List<C>, List<D>, List<E>, List<F>>> GetMultipleList<A, B, C, D, E, F>(string cmdText, Dictionary<string, string> parameters)
        {
            //IEnumerable<T> result;
            SqlMapper.GridReader result;
            Tuple<List<A>, List<B>, List<C>, List<D>, List<E>, List<F>> tuple;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor2", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor3", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor4", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor5", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var con = _connectionFactory.Connection;
            {
                result = await con.QueryMultipleAsync(cmdText, dyParam, commandType: CommandType.StoredProcedure);
            }
            var tabs = await result.ReadAsync<A>();
            var label = await result.ReadAsync<B>();
            var label1 = await result.ReadAsync<C>();
            var label2 = await result.ReadAsync<D>();
            var label3 = await result.ReadAsync<E>();
            var label4 = await result.ReadAsync<F>();
            tuple = Tuple.Create(tabs.AsList(), label.AsList(), label1.AsList(), label2.AsList(), label3.AsList(),label4.AsList());

            return await Task.FromResult(tuple);
        }


        public async virtual Task<Tuple<List<A>, List<B>, List<C>, List<D>, List<E>>> GetMultipleList<A, B, C, D, E>(string cmdText, Dictionary<string, string> parameters)
        {
            //IEnumerable<T> result;
            SqlMapper.GridReader result;
            Tuple<List<A>, List<B>, List<C>, List<D>,List<E>> tuple;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor2", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor3", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor4", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var con = _connectionFactory.Connection;
            {
                result = await con.QueryMultipleAsync(cmdText, dyParam, commandType: CommandType.StoredProcedure);
            }
            var tabs = await result.ReadAsync<A>();
            var label = await result.ReadAsync<B>();
            var label1 = await result.ReadAsync<C>();
            var label2 = await result.ReadAsync<D>();
            var label3 = await result.ReadAsync<E>();
            tuple = Tuple.Create(tabs.AsList(), label.AsList(), label1.AsList(), label2.AsList(),label3.AsList());

            return await Task.FromResult(tuple);
        }

        public async virtual Task<Tuple<List<A>, List<B>, List<C>, List<D>>> GetMultipleList11<A, B, C, D>(string cmdText, Dictionary<string, string> parameters)
        {
            //IEnumerable<T> result;
            SqlMapper.GridReader result;
            Tuple<List<A>, List<B>, List<C>, List<D>> tuple;
            var dyParam = new OracleDynamicParameters();
            foreach (var para in parameters)
            {
                dyParam.Add(name: para.Key, value: para.Value);
            }
            // parameters.Add("R_REF_CUR_ILC", : OracleDbType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            dyParam.Add(name: "refcursor1", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var con = _connectionFactory.Connection;
            {
                result = await con.QueryMultipleAsync(cmdText, dyParam, commandType: CommandType.StoredProcedure);
            }
            var tabs = await result.ReadAsync<A>();
            var label = await result.ReadAsync<B>();
            var table1 = await result.ReadAsync<C>();
            var table2 = await result.ReadAsync<D>();
            tuple = Tuple.Create(tabs.AsList(), label.AsList(), table1.AsList(), table2.AsList());

            return await Task.FromResult(tuple);
        }

        public async virtual Task<int> ExecuteQuery(string cmdText, Dictionary<string, string> parameters)
        {
            int result = 0;
            try
            {
                OracleDynamicParameters param = new OracleDynamicParameters();
                foreach (var pair in parameters) param.Add(pair.Key, pair.Value);

                var con = _connectionFactory.Connection;
                {
                    result = await con.ExecuteAsync(cmdText, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
