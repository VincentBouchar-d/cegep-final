﻿using System.Data.SqlClient;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace ProjetCegep.DAOs
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class Repository
    {
        #region AttributsPropriete

        /// <summary>
        /// La connexion.
        /// </summary>
        protected SqlConnection connexion;

        #endregion AttributsPropriete

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        protected Repository()
        {
            connexion = new SqlConnection("Server = 192.168.2.27; Database = Cegep; User Id=vincent;Password=Patate123");
        }

        #endregion Constructeurs

        #region MethodesService

        /// <summary>
        /// Méthode permettant d'ouvrir la connexion.
        /// </summary>
        protected void OuvrirConnexion()
        {
            connexion.Open();
        }

        /// <summary>
        /// Méthode permettant de fermer la connexion.
        /// </summary>
        protected void FermerConnexion()
        {
            connexion.Close();
        }

        #endregion MethodesService
    }
}
