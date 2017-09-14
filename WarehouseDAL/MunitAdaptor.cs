﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WarehouseDAL.DataContracts;
using System.Configuration;

namespace WarehouseDAL
{
    public class MunitAdaptor
    {
        private string getMunit = "[dbo].[GetMunit]";
        private string createOrUpdateMunit = "[dbo].[CreateOrUpdateMunit]";
        private string dicebleMunit = "[dbo].[DisableMunit]";
        private string getActiveMunit = "[dbo].[GetActiveMunit]";

        public IList<Munit> GetMunit()
        {
            return GetMunits(-1);
        }

        private IList<Munit> GetMunits(int id)
        {
            IList<Munit> munitList = null;
            using (var conn = new SqlConnection(ConnectionParameters.ConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(getMunit, conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var cmdParamId = new SqlParameter("id", System.Data.SqlDbType.Int);
                    cmdParamId.Value = id;
                    cmd.Parameters.Add(cmdParamId);

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        munitList = new List<Munit>();
                        while (reader.Read())
                        {
                            Munit newMunit = new Munit();
                            newMunit.Id = (int)reader["id"];
                            newMunit.MunitName = (string)reader["name"];
                            newMunit.IsActive = (bool)reader["IsActive"];
                            munitList.Add(newMunit);
                        }
                    }

                }

            }
            return munitList;
        }

        public Munit GetMunit(int id)
        {
            var munit = GetMunits(id);

            if (munit.Count > 0)
                return munit[0];
            return null;
        }


        public int CreateOrUpdateMunit(Munit munit)
        {
            int res = 0;
            using (var conn = new SqlConnection(ConnectionParameters.ConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(createOrUpdateMunit, conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var cmdParamId = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    if (munit.Id.HasValue)
                        cmdParamId.Value = munit.Id.Value;
                    else
                        cmdParamId.Value = DBNull.Value;

                    cmd.Parameters.Add(cmdParamId);

                    
                    SqlParameter pMunitName = new SqlParameter("@name", System.Data.SqlDbType.NVarChar, 10);
                    pMunitName.Value = munit.MunitName;
                    cmd.Parameters.Add(pMunitName);


                    SqlParameter pResult = new SqlParameter("@status", System.Data.SqlDbType.Int);
                    pResult.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(pResult);

                    cmd.ExecuteNonQuery();


                    res = Convert.ToInt32(pResult.Value);


                }
                return res;
            }
        }


        public int DisableMunit(int id)
        {
            int res = -10;
            using (var conn = new SqlConnection(ConnectionParameters.ConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(dicebleMunit, conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter pId = new SqlParameter("id", System.Data.SqlDbType.Int);
                    pId.Value = id;
                    cmd.Parameters.Add(pId);


                    SqlParameter pResult = new SqlParameter("status", System.Data.SqlDbType.Int);
                    pResult.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(pResult);


                    cmd.ExecuteNonQuery();


                    res = Convert.ToInt32(pResult.Value);

                }
                return res;
            }
        }

        public IList<Munit> GetActiveMunit()
        {
            IList<Munit> munitList = null;
            using (var conn = new SqlConnection(ConnectionParameters.ConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(getActiveMunit, conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        munitList = new List<Munit>();
                        while (reader.Read())
                        {
                            Munit newMunit = new Munit();
                            newMunit.Id = (int)reader["id"];
                            newMunit.MunitName = (string)reader["name"];
                            newMunit.IsActive = (bool)reader["isActive"];
                            munitList.Add(newMunit);
                        }
                    }

                }

            }
            return munitList;
        }
    }
}
