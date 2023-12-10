using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using ProjetCegep.DTOs;
using ProjetCegep.Controleurs;

namespace applicationProjetCegep
{
    [Activity(Label = "Modifier cours")]
    public class ModifierCoursActivity : AppCompatActivity
    {
        private TextView lblNomCours;
        private EditText edtNoCours;
        private EditText edtDescriptionCours;
        

        private Button btnModifierCours;

        private CoursDTO coursDTO;
        private CoursDTO coursModif;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ModifierCours);

            lblNomCours = FindViewById<TextView>(Resource.Id.lblNomCours);
            edtNoCours = FindViewById<EditText>(Resource.Id.edtNoCours);
            edtDescriptionCours = FindViewById<EditText>(Resource.Id.edtDescriptionCours);
           
            btnModifierCours = FindViewById<Button>(Resource.Id.btnModifierCours);
            coursDTO = CegepControleur.Instance.ObtenirCours(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), Intent.GetStringExtra("paramNomCours"));
            
            
            btnModifierCours.Click += delegate
            {
                CegepControleur.Instance.ModifierCours(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), new CoursDTO(edtNoCours.Text, lblNomCours.Text,edtDescriptionCours.Text));
                Finish();

            };
        }



        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

        private void RafraichirDonnees()
        {
            lblNomCours.Text = coursDTO.Nom;
            edtNoCours.Text = coursDTO.No;
            edtDescriptionCours.Text = coursDTO.Description;   
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_modifierEnseignant, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuRetour:
                    Finish();
                    break;
                case Resource.Id.menuQuitter:
                    FinishAffinity();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}