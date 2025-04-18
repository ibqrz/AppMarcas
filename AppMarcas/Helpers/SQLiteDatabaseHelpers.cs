using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using AppMarcas.Models;

namespace AppMarcas.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelpers(string path) 
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Marca>().Wait();
        }

        public Task<int> Insert(Marca p) 
        {
           return _conn.InsertAsync(p);
        }

        public Task<List<Marca>> Update(Marca p) 
        {
            string sql = "UPDATE Marca SET marNome=? and marObservacoes=? WHERE marId=?";
            
            return _conn.QueryAsync<Marca>(sql, p.marNome, p.marObservacoes, p.marId);
        }

        public Task<int> Delete(int p) 
        { 
            return _conn.Table<Marca>().DeleteAsync(i => i.marId == p);

            /* ou
            string sql = "DELETE Marca WHERE marId=?";
            return _conn.QueryAsync<Marca>(sql, p); */
        }

        public Task<List<Marca>> GetAll() 
        { 
            return _conn.Table<Marca>().ToListAsync();         
        }

        public Task<List<Marca>> Search(string p) 
        {
            string sql = "SELECT * FROM Marca WHERE marNome LIKE %'" + p + "'%";

            return _conn.QueryAsync<Marca>(sql);
        }
    }
}
