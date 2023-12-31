﻿using Android.App;
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
    public class CreerEnseignantActivity : AppCompatActivity
    {
        /// <summary>
        /// bouton pour ajouter un enseignant
        /// </summary>
        private Button btnAjouterEnseignant;
        /// <summary>
        /// Liste contenant les enseignantDTO
        /// </summary>
        private EnseignantDTO[] listeEnseignant;
        /// <summary>
        /// Adpateur pour la listeEnseignant
        /// </summary>
        private ListeEnseignantAdapteur adapteurListeEnseignant;
        /// <summary>
        /// Listeview contenant une liste des enseignants du programme
        /// </summary>
        private ListView listeVueEnseignant;

        /// <summary>
        /// EditText contentant le numéro de l'enseignant
        /// </summary>
        private EditText edtNoEnseignant;
        /// <summary>
        /// EditText contentant le nom de l'enseignant
        /// </summary>
        private EditText edtNomEnseignant;
        /// <summary>
        /// EditText contentant le prénom de l'enseignant
        /// </summary>
        private EditText edtPrenomEnseignant;
        /// <summary>
        /// EditText contentant l'adresse de l'enseignant
        /// </summary>
        private EditText edtAdresseEnseignant;
        /// <summary>
        /// EditText contentant la ville de l'enseignant
        /// </summary>
        private EditText edtVilleEnseignant;
        /// <summary>
        /// EditText contentant la province de l'enseignant
        /// </summary>
        private EditText edtProvinceEnseignant;
        /// <summary>
        /// EditText contentant le code postal de l'enseignant
        /// </summary>
        private EditText edtCodePostalEnseignant;
        /// <summary>
        /// EditText contentant le numéro de téléphone de l'enseignant
        /// </summary>
        private EditText edtTelephoneEnseignant;
        /// <summary>
        /// EditText contentant le courriel de l'enseignant
        /// </summary>
        private EditText edtCourrielEnseignant;

        /// <summary>
        /// Fonciton OnCreate qui s'exécute lors du lancement de l'activité
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AjouterEnseignant);


            btnAjouterEnseignant = FindViewById<Button>(Resource.Id.btnAjouterEnseignant);
            listeVueEnseignant = FindViewById<ListView>(Resource.Id.listViewEnseignant);
            edtNoEnseignant = FindViewById<EditText>(Resource.Id.edtNoEnseignant);
            edtNomEnseignant = FindViewById<EditText>(Resource.Id.edtNomEnseignant);
            edtPrenomEnseignant = FindViewById<EditText>(Resource.Id.edtPrenomEnseignant);
            edtAdresseEnseignant = FindViewById<EditText>(Resource.Id.edtAdresseEnseignant);
            edtVilleEnseignant = FindViewById<EditText>(Resource.Id.edtVilleEnseignant);
            edtProvinceEnseignant = FindViewById<EditText>(Resource.Id.edtProvinceEnseignant);
            edtCodePostalEnseignant = FindViewById<EditText>(Resource.Id.edtCodePostalEnseignant);
            edtTelephoneEnseignant = FindViewById<EditText>(Resource.Id.edtTelephoneEnseignant);
            edtCourrielEnseignant = FindViewById<EditText>(Resource.Id.edtCourrielEnseignant);


            // Bouton permettant d'ajouter un enseignant
            btnAjouterEnseignant.Click += delegate
            {
                if ((edtAdresseEnseignant.Text.Length > 0) && (edtVilleEnseignant.Text.Length > 0) && (edtProvinceEnseignant.Text.Length > 0) && (edtCodePostalEnseignant.Text.Length > 0) && (edtTelephoneEnseignant.Text.Length > 0) && (edtCourrielEnseignant.Text.Length > 0) && (edtNoEnseignant.Text.Length > 0) && (edtPrenomEnseignant.Text.Length > 0))
                {
                    try
                    {
                        string nom = edtNomEnseignant.Text;
                        CegepControleur.Instance.AjouterEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), new EnseignantDTO(int.Parse(edtNoEnseignant.Text), edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text));
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
            // Permet de lancer l'activité AfficherEnseignantActivity lorsqu'un enseignant est cliqué dans le listView
            listeVueEnseignant.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                int numeroEmploye = listeEnseignant[e.Position].NoEmploye;

                Intent activiteAfficherEnseignant = new Intent(this, typeof(AfficherEnseignantActivity));
                //On initialise les paramètres avant de lancer la nouvelle activité.
                activiteAfficherEnseignant.PutExtra("paramNomCegep", Intent.GetStringExtra("paramNomCegep"));
                activiteAfficherEnseignant.PutExtra("paramNomDepartement", Intent.GetStringExtra("paramNomDepartement"));
                activiteAfficherEnseignant.PutExtra("paramNoEmploye", listeEnseignant[e.Position].NoEmploye);
                //On démarre la nouvelle activité.
                StartActivity(activiteAfficherEnseignant);
            };
        }
        /// <summary>
        /// Fonciton OnResume qui s'exécute lorsque l'application recommence
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
            listeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement")).ToArray();
            adapteurListeEnseignant = new ListeEnseignantAdapteur(this, listeEnseignant);
            listeVueEnseignant.Adapter = adapteurListeEnseignant;
        }


        ///// <summary>
        ///// Initialise le menu de l'activité principale.///// </summary>
        ///// <param name="menu">Le menu à construire.</param>
        ///// <returns>Retourne True si l'optionMenu est bien créé.</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_creerEnseignant, menu);
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
                    foreach (EnseignantDTO enseignant in listeEnseignant)
                    {
                        CegepControleur.Instance.SupprimerEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), enseignant.NoEmploye);
                    }
                    RafraichirDonnees();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}