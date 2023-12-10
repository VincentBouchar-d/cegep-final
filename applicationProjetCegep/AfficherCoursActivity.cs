using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Android.Widget;
using Javax.Security.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Service.Voice.VoiceInteractionSession;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using applicationProjetCegep.Adapteurs;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using ProjetCegep.Utils;

namespace applicationProjetCegep
{
    [Activity(Label = "AfficherCours")]
    public class AfficherCoursActivity : AppCompatActivity
    {
        
        
        CoursDTO  coursDTO;
        private TextView lblNoCours;
        private TextView lblNomCours;
        private TextView lblDescriptionCours;
        
        
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AfficherCours);

            

            lblNoCours = FindViewById<TextView>(Resource.Id.lblNoCours);
            lblNomCours = FindViewById<TextView>(Resource.Id.lblNomCours);
            lblDescriptionCours = FindViewById<TextView>(Resource.Id.lblDescriptionCours);
            



            RafraichirDonnees();
        }

        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

        private void RafraichirDonnees()
        {
            string nomCegep = Intent.GetStringExtra("paramNomCegep");
            string nomDepartement = Intent.GetStringExtra("paramNomDepartement");
            string nomCours = Intent.GetStringExtra("paramNomCours");

            coursDTO = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);


            lblNoCours.Text = coursDTO.No;
            lblNomCours.Text = coursDTO.Nom;
            lblDescriptionCours.Text = coursDTO.Description;
            


        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_afficherCours, menu);
            return base.OnCreateOptionsMenu(menu);

        }

        




        /// <summary>/// Évenement exécuté lors d'un choix dans le menu./// </summary>
        /// <param name="item">L'item sélectionné.</param>
        /// <returns>Retourne si un option à été sélectionné avec succès.</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuSupprimer:
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetPositiveButton("NON", (sender, args) => { Finish(); });
                    builder.SetNegativeButton("OUI", (sender, args) => {

                        CegepControleur.Instance.SupprimerCours(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), Intent.GetStringExtra("paramNomCours"));
                        Finish();
                    });
                    AlertDialog dialog = builder.Create();
                    dialog.SetTitle("*** ATTENTION  ***");
                    dialog.SetMessage("Voulez-vous VRAIMENT supprimer ce cours? Cette action est irréversible.");
                    dialog.Window.SetGravity(GravityFlags.Center);
                    dialog.Show();
                    break;
                case Resource.Id.menuModifier:
                    var ModifierCoursActivity = new Intent(this, typeof(ModifierCoursActivity));
                    ModifierCoursActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    ModifierCoursActivity.PutExtra("paramNomDepartement", Intent.GetStringExtra("paramNomDepartement"));
                    ModifierCoursActivity.PutExtra("paramNomCours", Intent.GetStringExtra("paramNomCours"));
                    StartActivity(ModifierCoursActivity); 
                    break;
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