using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjetCegep.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace applicationProjetCegep.Adapteurs
{
    public class ListeEnseignantAdapteur : BaseAdapter<EnseignantDTO>
    {
        /// <summary>
        /// Variable qui représente l'activité
        /// </summary>
        private Activity context;
        /// <summary>
        /// Variable qui représente la liste des enseignants
        /// </summary>
        private EnseignantDTO[] listeEnseignant;
        /// <summary>
        /// Fonction qui donne les valeurs aux varialbes context et listeEnseignant
        /// </summary>
        /// <param name="uneActivity"></param>
        /// <param name="uneListeEnseignantDTO"></param>
        public ListeEnseignantAdapteur(Activity uneActivity, EnseignantDTO[] uneListeEnseignantDTO)
        {
            context = uneActivity;
            listeEnseignant = uneListeEnseignantDTO;
        }
        /// <summary>
        /// Fonction qui retourne l'enseugnant à la position donnée
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override EnseignantDTO this[int position]
        {
            get
            {
                return listeEnseignant[position];
            }
        }
        /// <summary>
        /// Fonction qui retourne le nombre d'enseignant dans la liste
        /// </summary>
        public override int Count
        {
            get
            {
                return listeEnseignant.Length;
            }
        }
        /// <summary>
        /// fonction qui retourne la position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override long GetItemId(int position)
        {
            return position;
        }
        /// <summary>
        /// Fonction qui détermine l'affichage des enseignants dans un listView
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.listeEnseignantItems, null);
            view.FindViewById<TextView>(Resource.Id.tvNom).Text = listeEnseignant[position].NoEmploye + "   " + listeEnseignant[position].Prenom + " " + listeEnseignant[position].Nom;
            return view;
        }
    }
}