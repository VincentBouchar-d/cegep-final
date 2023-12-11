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
    [Activity(Label = "AfficherEnseignant")]
    public class AfficherEnseignantActivity : AppCompatActivity
    {
        
        
        /// <summary>
        /// Label contenant le numéro de l'enseignant
        /// </summary>
        private TextView lblNoEnseignant;
        /// <summary>
        /// Label contenant le nom de l'enseignant
        /// </summary>
        private TextView lblNomEnseignant;
        /// <summary>
        /// Label contenant le prénom de l'enseignant
        /// </summary>
        private TextView lblPrenomEnseignant;
        /// <summary>
        /// Label contenant la ville de l'enseignant
        /// </summary>
        private TextView lblVilleEnseignant;
        /// <summary>
        /// Label contenant le code postal de l'enseignant
        /// </summary>
        private TextView lblCodePostalEnseignant;
        /// <summary>
        /// Label contenant l'adresse de l'enseignant
        /// </summary>
        private TextView lblAdresseEnseignant;
        /// <summary>
        /// Label contenant la province de l'enseignant
        /// </summary>
        private TextView lblProvinceEnseignant;
        /// <summary>
        /// Label contenant le numéro de téléphone de l'enseignant
        /// </summary>
        private TextView lblTelephoneEnseignant;
        /// <summary>
        /// Label contenant l'adresse de l'enseignant
        /// </summary>
        private TextView lblCourrielEnseignant;
        /// <summary>
        /// Objet contenant les informations de l'enseignant
        /// </summary>
        private EnseignantDTO enseignantDTO;

        
        
        
        /// <summary>
        /// Fonction OnCreate qui s'exécute lorsque l'activité démarre
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AfficherEnseignant);

            

            lblNoEnseignant = FindViewById<TextView>(Resource.Id.lblNoEnseignant);
            lblNomEnseignant = FindViewById<TextView>(Resource.Id.lblNomEnseignant);
            lblPrenomEnseignant = FindViewById<TextView>(Resource.Id.lblPrenomEnseignant);
            lblAdresseEnseignant = FindViewById<TextView>(Resource.Id.lblAdresseEnseignant);
            lblVilleEnseignant = FindViewById<TextView>(Resource.Id.lblVilleEnseignant);
            lblProvinceEnseignant = FindViewById<TextView>(Resource.Id.lblProvinceEnseignant);
            lblCodePostalEnseignant = FindViewById<TextView>(Resource.Id.lblCodePostalEnseignant);
            lblTelephoneEnseignant = FindViewById<TextView>(Resource.Id.lblTelephoneEnseignant);
            lblCourrielEnseignant = FindViewById<TextView>(Resource.Id.lblCourrielEnseignant);







            RafraichirDonnees();
        }
        /// <summary>
        /// Fonction OnResume qui s'exécute lorsque l'activité sort d'une pause
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }
        /// <summary>
        /// Fonction qui permet d'afficher les bonnes informations dans les labels
        /// </summary>
        private void RafraichirDonnees()
        {
            string nomCegep = Intent.GetStringExtra("paramNomCegep");
            string nomDepartement = Intent.GetStringExtra("paramNomDepartement");
            int noEmploye = Intent.GetIntExtra("paramNoEmploye", 0);

            enseignantDTO = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEmploye);


            lblNoEnseignant.Text = enseignantDTO.NoEmploye.ToString();
            lblNomEnseignant.Text = enseignantDTO.Nom;
            lblPrenomEnseignant.Text = enseignantDTO.Prenom;
            lblAdresseEnseignant.Text = enseignantDTO.Adresse;
            lblVilleEnseignant.Text = enseignantDTO.Ville;
            lblProvinceEnseignant.Text = enseignantDTO.Province;
            lblCodePostalEnseignant.Text = enseignantDTO.CodePostal;
            lblTelephoneEnseignant.Text = enseignantDTO.Telephone;
            lblCourrielEnseignant.Text = enseignantDTO.Courriel;


        }

        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_afficherEnseignant, menu);
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

                        CegepControleur.Instance.SupprimerEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), Intent.GetIntExtra("paramNoEmploye", 0));
                        Finish();
                    });
                    AlertDialog dialog = builder.Create();
                    dialog.SetTitle("*** ATTENTION  ***");
                    dialog.SetMessage("Voulez-vous VRAIMENT supprimer cet enseignant ? Cette action est irréversible.");
                    dialog.Window.SetGravity(GravityFlags.Center);
                    dialog.Show();
                    break;
                case Resource.Id.menuModifier:
                    var ModifierEnseignantActivity = new Intent(this, typeof(ModifierEnseignantActivity));
                    ModifierEnseignantActivity.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                    ModifierEnseignantActivity.PutExtra("paramNomDepartement", Intent.GetStringExtra("paramNomDepartement"));
                    ModifierEnseignantActivity.PutExtra("paramNoEmploye", Intent.GetIntExtra("paramNoEmploye", 0));
                    StartActivity(ModifierEnseignantActivity);
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