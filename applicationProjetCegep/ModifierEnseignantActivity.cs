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
    [Activity(Label = "Modifier Enseignant")]
    public class ModifierEnseignantActivity : AppCompatActivity
    {
        private TextView lblNoEmploye;
        private EditText edtNomEnseignant;
        private EditText edtPrenomEnseignant;
        private EditText edtAdresseEnseignant;
        private EditText edtVilleEnseignant;
        private EditText edtProvinceEnseignant;
        private EditText edtCodePostalEnseignant;
        private EditText edtTelephoneEnseignant;
        private EditText edtCourrielEnseignant;

        private Button btnModifierEnseignant;

        private EnseignantDTO enseignantDTO;
        private EnseignantDTO enseignantModif;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ModifierEnseignant);

            lblNoEmploye = FindViewById<TextView>(Resource.Id.lblNoEmploye);
            edtNomEnseignant = FindViewById<EditText>(Resource.Id.edtNomEnseignant);
            edtPrenomEnseignant = FindViewById<EditText>(Resource.Id.edtPrenomEnseignant);
            edtAdresseEnseignant = FindViewById<EditText>(Resource.Id.edtAdresseEnseignant);
            edtVilleEnseignant = FindViewById<EditText>(Resource.Id.edtVilleEnseignant);
            edtProvinceEnseignant = FindViewById<EditText>(Resource.Id.edtProvinceEnseignant);
            edtCodePostalEnseignant = FindViewById<EditText>(Resource.Id.edtCodePostalEnseignant);
            edtTelephoneEnseignant = FindViewById<EditText>(Resource.Id.edtTelephoneEnseignant);
            edtCourrielEnseignant = FindViewById<EditText>(Resource.Id.edtCourrielEnseignant);
            btnModifierEnseignant = FindViewById<Button>(Resource.Id.btnModifierEnseignant);
            enseignantDTO = CegepControleur.Instance.ObtenirEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), Intent.GetIntExtra("paramNoEmploye", 0));
            
            
            btnModifierEnseignant.Click += delegate
            {
                CegepControleur.Instance.ModifierEnseignant(Intent.GetStringExtra("paramNomCegep"), Intent.GetStringExtra("paramNomDepartement"), new EnseignantDTO( int.Parse(lblNoEmploye.Text) , edtNomEnseignant.Text, edtPrenomEnseignant.Text, edtAdresseEnseignant.Text, edtVilleEnseignant.Text, edtProvinceEnseignant.Text, edtCodePostalEnseignant.Text, edtTelephoneEnseignant.Text, edtCourrielEnseignant.Text));
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
            
            
            lblNoEmploye.Text = enseignantDTO.NoEmploye.ToString();
            edtNomEnseignant.Text = enseignantDTO.Nom;
            edtPrenomEnseignant.Text = enseignantDTO.Prenom;
            edtAdresseEnseignant.Text = enseignantDTO.Adresse;
            edtVilleEnseignant.Text = enseignantDTO.Ville;
            edtProvinceEnseignant.Text = enseignantDTO.Province;
            edtCodePostalEnseignant.Text = enseignantDTO.CodePostal;
            edtTelephoneEnseignant.Text = enseignantDTO.Telephone;
            edtCourrielEnseignant.Text = enseignantDTO.Courriel;
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