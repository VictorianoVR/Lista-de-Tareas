﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace listadoTareas
{
    public static class Datos
    {
        public static void CrearRegistro(TareaModel model)
        {
            string query = $"insert into dbo.Tareas(Asignatura, Mandato, FechaAsignacion, FechaEntrega, Descripcion) values(@Asignatura, @Mandato, @FechaAsignacion, @FechaEntrega, @Descripcion)";

            using (SqlConnection con = new SqlConnection("Data source=PC\\VVV;initial catalog=listadoTarea;User iD=sa;Password=123456"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Asignatura", model.Asignatura);
                cmd.Parameters.AddWithValue("@Mandato", model.Mandato);
                cmd.Parameters.AddWithValue("@FechaAsignacion", model.FechaAsignacion);
                cmd.Parameters.AddWithValue("@FechaEntrega", model.FechaEntrega);
                cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static List<TareaModel> GetList()
        {
            var result = new List<TareaModel>();
            string query = $"select * from dbo.Tareas";

            TareaModel model = null;
            try
            {
                using (SqlConnection con = new SqlConnection("Data source=PC\\VVV;initial catalog=listadoTarea;User iD=sa;Password=123456"))
                {
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = reader.GetModel<TareaModel>();
                            result.Add(model);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception e)
            {
            }
            return result;
        }

        public static void Eliminar(int id)
        {
            string query = $"delete dbo.Tareas where Id = {id}";

            using (SqlConnection con = new SqlConnection("Data source=PC\\VVV;initial catalog=listadoTarea;User iD=sa;Password=123456"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void ActivarInactivar(int id, string estado)
        {
            string activo = "Si";
            if (estado == "Si")
                activo = "No";

            string query = $"update dbo.Tareas set Activo = '{activo}' where Id = {id}";

            using (SqlConnection con = new SqlConnection("Data source=PC\\VVV;initial catalog=listadoTarea;User iD=sa;Password=123456"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static T GetModel<T>(this SqlDataReader reader) where T : class
        {
            T model = Activator.CreateInstance<T>();

            foreach (var item in typeof(T).GetProperties().Where(p => p.CanWrite && p.GetCustomAttributes(typeof(ColumnSqlAttribute), false).Any()))
            {
                var column = item.GetCustomAttributes(typeof(ColumnSqlAttribute), false).FirstOrDefault() as ColumnSqlAttribute;

                if (item.PropertyType == typeof(string))
                    item.SetValue(model, Convert.ToString(reader[column.NameColumn]));
                else if (item.PropertyType == typeof(DateTime))
                    item.SetValue(model, Convert.ToDateTime(reader[column.NameColumn] is DBNull ? DateTime.Now : reader[column.NameColumn]));
                else if (item.PropertyType == typeof(double))
                    item.SetValue(model, Convert.ToDouble(reader[column.NameColumn] is DBNull ? 0 : reader[column.NameColumn]));
                else if (item.PropertyType == typeof(int))
                    item.SetValue(model, Convert.ToInt32(reader[column.NameColumn] is DBNull ? 0 : reader[column.NameColumn]));

                EvaluateDateTime<T>(item, reader, column, model);
            }

            return model;
        }

        private static void EvaluateDateTime<T>(PropertyInfo item, SqlDataReader reader, ColumnSqlAttribute column, T model)
        {
            if (item.PropertyType == typeof(DateTime?))
            {
                if (reader[column.NameColumn] is DBNull)
                {
                    //
                }
                else
                    item.SetValue(model, Convert.ToDateTime(reader[column.NameColumn]));
            }
        }
    }

    public class ColumnSqlAttribute : Attribute
    {
        public string NameColumn { get; set; }

        public ColumnSqlAttribute(string nameColumn)
        {
            NameColumn = nameColumn;
        }
    }
}