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
    [Activity(Label = "AfficherDepartement")]
    public class AfficherDepartementActivity : AppCompatActivity
    {
        
        

        private TextView lblNomDepartement;
        private TextView lblNumeroDepartement;
        private TextView lblDescriptionDepartement;
        private DepartementDTO departementDTO;

        
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AfficherDepartement);


            
            lblNomDepartement = FindViewById<TextView>(Resource.Id.lblNomDepartement);
            lblNumeroDepartement = FindViewById<TextView>(Resource.Id.lblNumeroDepartement);
            lblDescriptionDepartement = FindViewById<TextView>(Resource.Id.lblDescriptionDepartement);






            RafraichirDonnees();
        }

        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

        private void RafraichirDonnees()
        {
            
            departementDTO = CegepControleur.Instance.ObtenirDepartement(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"));

            lblNomDepartement.Text = departementDTO.Nom;
            lblNumeroDepartement.Text = departementDTO.No;
            lblDescriptionDepartement.Text = departementDTO.Description;
     
        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_afficherDepartement, menu);
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
                        
                            CegepControleur.Instance.SupprimerDepartement(Intent.GetStringExtra("paramNomCegep"), departementDTO.Nom);
                            Finish();
                    });
                    AlertDialog dialog = builder.Create();
                    dialog.SetTitle("*** ATTENTION  ***");
                    dialog.SetMessage("Voulez-vous VRAIMENT supprimer ce département ? Cette action est irréversible.");
                    dialog.Window.SetGravity(GravityFlags.Center);
                    dialog.Show();
                    
                    break;
                case Resource.Id.menuRetour:
                    Finish();
                    break;
                case Resource.Id.menuCreerEnseignant:
                    var CreerEnseignantActivity = new Intent(this, typeof(CreerEnseignantActivity));
                    CreerEnseignantActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    CreerEnseignantActivity.PutExtra("paramNomDepartement", departementDTO.Nom);
                    StartActivity(CreerEnseignantActivity);
                    break;
                case Resource.Id.menuQuitter:
                    FinishAffinity();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        
        
    }
}