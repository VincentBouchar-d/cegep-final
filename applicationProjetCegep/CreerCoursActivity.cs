using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using applicationProjetCegep.Adapteurs;
using ProjetCegep.Controleurs;
using ProjetCegep.DTOs;
using ProjetCegep.Utils;
using System;
using static Android.Service.Voice.VoiceInteractionSession;
using Javax.Security.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;


namespace applicationProjetCegep
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class CreerCoursActivity : AppCompatActivity
    {
        /// <summary>
        /// bouton pour ajouter un enseignant
        /// </summary>
        private Button btnAjouterCours;
        /// <summary>
        /// Liste contenant les enseignantDTO
        /// </summary>
        private CoursDTO[] listeCours;
        /// <summary>
        /// Adpateur pour la listeEnseignant
        /// </summary>
        private ListeCoursAdapteur adapteurListeCours;

        private ListView listeVueCours;
        private EditText edtNoCours;
        private EditText edtNomCours;
        private EditText edtDescriptionCours;
      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AjouterCours);


            btnAjouterCours = FindViewById<Button>(Resource.Id.btnAjouterCours);
            listeVueCours = FindViewById<ListView>(Resource.Id.listViewCours);
            edtNoCours = FindViewById<EditText>(Resource.Id.edtNoCours);
            edtNomCours = FindViewById<EditText>(Resource.Id.edtNomCours);
            edtDescriptionCours = FindViewById<EditText>(Resource.Id.edtDescriptionCours);
            


            btnAjouterCours.Click += delegate
            {
                if ((edtNomCours.Text.Length > 0) && (edtNoCours.Text.Length > 0) && (edtDescriptionCours.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomCours.Text;
                        CegepControleur.Instance.AjouterCours(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), new CoursDTO(edtNoCours.Text, edtNomCours.Text, edtDescriptionCours.Text));
                        RafraichirDonnees();
                        DialoguesUtils.AfficherToasts(this, nom + " ajouté !!!");
                    }
                    catch (Exception ex)
                    {
                        DialoguesUtils.AfficherMessageOK(this, "Erreur", ex.Message);
                    }
                }
                else
                    DialoguesUtils.AfficherMessageOK(this, "Erreur", "Veuillez remplir tous les champs...");
            };

            listeVueCours.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                string nomCours = listeCours[e.Position].Nom;

                Intent activiteAfficherCours = new Intent(this, typeof(AfficherCoursActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteAfficherCours.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                activiteAfficherCours.PutExtra("paramNomDepartement", Intent.GetStringExtra("paramNomDepartement"));
                activiteAfficherCours.PutExtra("paramNomCours", listeCours[e.Position].Nom);
                //On démarre la nouvelle activité.
                StartActivity(activiteAfficherCours);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            RafraichirDonnees();
        }

        private void RafraichirDonnees()
        {
            listeCours = CegepControleur.Instance.ObtenirListeCours(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement")).ToArray();
            adapteurListeCours = new ListeCoursAdapteur(this, listeCours);
            listeVueCours.Adapter = adapteurListeCours;
        }


        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_creerCours, menu);
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