using ProjetCegep.Modeles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetCegep.DAOs;
using ProjetCegep.DTOs;
using ProjetCegep.Controleurs;
namespace ProjetCegep
{
    [TestClass]
    public class testViderDepartement
    {
        [TestMethod]
        public void TestViderDepartement()
        {
            Cegep cegep = new Cegep();
            Assert.AreEqual(cegep.Nom, ""); 
            Assert.AreEqual(cegep.Adresse, "");
            Assert.AreEqual(cegep.Telephone, "");
            Assert.AreEqual(cegep.Courriel, "");
            Assert.AreEqual(cegep.Ville, "");
            Assert.AreEqual(cegep.Province, "");
            Assert.AreEqual(cegep.CodePostal, "");


            DepartementDTO departement = new DepartementDTO();
            Assert.AreEqual(departement.Nom, "");
            Assert.AreEqual(departement.No, "");
            Assert.AreEqual(departement.Description, "");

            CegepControleur.Instance.AjouterDepartement(cegep.Nom, departement);
            
            try
            {
                CegepControleur.Instance.ViderDepartement(cegep.Nom);
            }
            catch { }
        }
    }
}