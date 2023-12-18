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
        /// bouton pour ajouter un Cours
        /// </summary>
        private Button btnAjouterCours;
        /// <summary>
        /// Liste contenant les CoursDTO
        /// </summary>
        private CoursDTO[] listeCours;
        /// <summary>
        /// Adpateur pour la listeCours
        /// </summary>
        private ListeCoursAdapteur adapteurListeCours;
        /// <summary>
        /// ListView pour afficher la liste des cours
        /// </summary>
        private ListView listeVueCours;
        /// <summary>
        /// Label contenant le numéro du cours
        /// </summary>
        private EditText edtNoCours;
        /// <summary>
        /// Label contenant le nom du cours
        /// </summary>
        private EditText edtNomCours;
        /// <summary>
        /// Label contenant la description du cours
        /// </summary>
        private EditText edtDescriptionCours;
      
        /// <summary>
        /// Fonction OnCreate qui s'exécute lorsque l'activité se lance
        /// </summary>
        /// <param name="savedInstanceState"></param>
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
            

            // Bouton permettant d'ajouter un cours
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
            //Permet de lancer l'activité AfficherCoursActivity lorsque l'on clique sur un des cours du listView
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
        /// <summary>
        /// Fonction OnResume qui s'exécute lorsque l'activité recommence après une pause
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
                case Resource.Id.menuVider:
                    CegepControleur.Instance.ViderCours(Intent.GetStringExtra("paramNomCegep"),Intent.GetStringExtra("paramNomDepartement"));
                    RafraichirDonnees();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}