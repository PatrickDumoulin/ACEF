using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.BOL;
using DataAccess.BOL.CowMilk;
using DataModels.BOL;
using DataModels.BOL.Production;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class InitialTests
    {
        public InitialTests() { }

        public bool RunTests()
        {
            bool allSuccess = true;

            allSuccess &= getFromAnotherBLL();
            
            allSuccess &= getListBOL();
            allSuccess &= getBOLWithChilds();

            allSuccess &= addNewBOLWithoutChilds();
            allSuccess &= addNewBOLWithChilds();

            allSuccess &= deleteBOLWithChilds();
            allSuccess &= deleteBOLWithoutChilds();

            allSuccess &= updateBOLWithChilds();
            allSuccess &= updateBOLWithoutChilds();
            
            return allSuccess;
        }

        #region Get
        private bool getListBOL()
        {
            int idPriceList = 1;
            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);
            List<ICowMilkIngredientPriceListMonthBOL> results = cowMilkBLL.GetIngredientPriceListMonthsFromPriceList(idPriceList).ElementList;

            bool allSuccess = results.Any();
            foreach(ICowMilkIngredientPriceListMonthBOL bol in results)
            {
                decimal decryptedAmount = bol.Amount;
                allSuccess &= (bol.Amount != 0);
            }


            return allSuccess;
        }
        private bool getBOLWithChilds()
        {
            int idElement = 6;
            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientBOL cowMilkIngredientBOL = cowMilkBLL.GetIngredient(idElement).Element;

            return cowMilkIngredientBOL != null && cowMilkIngredientBOL.PriceLists.GetAllElements().All(x => x.IdCowMilkIngredient == idElement);
        }
        #endregion

        #region Add
        private bool addNewBOLWithoutChilds()
        {
            int idPriceList  = 1;
            int idIngredient = 6;

            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientPriceListMonthBOL newElement = new CowMilkIngredientPriceListMonthBOL();
            newElement.IdCowMilkIngredient = idIngredient;
            newElement.IdCowMilkIngredientPriceList = idPriceList;
            newElement.DCPercentage = 20;
            newElement.Amount = 50;
            newElement.EffectiveMonthDate = DateTime.Now;
            
            GenericResponse saveResponse = cowMilkBLL.SaveIngredientPriceListMonth(newElement);

            ICowMilkIngredientPriceListMonthBOL dbElement = cowMilkBLL.GetIngredientPriceListMonth(newElement.Id).Element;
            return saveResponse.Succeeded && isEqualElement(newElement, dbElement);
        }
        private bool addNewBOLWithChilds()
        {
            int idPriceList = 1;
            int idProductionTypeDepartment = 1;

            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientBOL newElement = new CowMilkIngredientBOL();
            newElement.DefaultDcperTon = 30;
            newElement.DisplayName = "newDisplayName";
            newElement.IdProductionTypeDepartment = idProductionTypeDepartment;
            newElement.Name = "newName";
            newElement.UnitDisplayName = "newUnitDisplayName";

            ICowMilkIngredientPriceListMonthBOL newElementChild = new CowMilkIngredientPriceListMonthBOL();
            newElementChild.IdCowMilkIngredient = newElement.Id;
            newElementChild.IdCowMilkIngredientPriceList = idPriceList;
            newElementChild.DCPercentage = 20;
            newElementChild.Amount = 50;
            newElementChild.EffectiveMonthDate = DateTime.Now;

            newElement.PriceLists.Add(newElementChild);
            GenericResponse saveResponse = cowMilkBLL.SaveIngredient(newElement);

            ICowMilkIngredientBOL dbElement = cowMilkBLL.GetIngredient(newElement.Id).Element;
            return saveResponse.Succeeded && isEqualElement(newElement, dbElement);
        }
        #endregion

        #region Delete
        private bool deleteBOLWithoutChilds()
        {
            int idPriceList = 1;
            int idIngredient = 6;
            bool success = false;

            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientPriceListMonthBOL newElement = new CowMilkIngredientPriceListMonthBOL();
            newElement.IdCowMilkIngredient = idIngredient;
            newElement.IdCowMilkIngredientPriceList = idPriceList;
            newElement.DCPercentage = 20;
            newElement.Amount = 50;
            newElement.EffectiveMonthDate = DateTime.Now;

            GenericResponse saveResponse = cowMilkBLL.SaveIngredientPriceListMonth(newElement);

            if(saveResponse.Succeeded)
            {
                ICowMilkIngredientPriceListMonthBOL dbElement = cowMilkBLL.GetIngredientPriceListMonth(newElement.Id).Element;
                dbElement.State = CoreLib.Definitions.ObjectState.DELETED;
                saveResponse = cowMilkBLL.SaveIngredientPriceListMonth(dbElement);

                dbElement = cowMilkBLL.GetIngredientPriceListMonth(newElement.Id).Element;
                success = saveResponse.Succeeded && dbElement.UntypedRecord == null;
            }

            return success;
        }
        private bool deleteBOLWithChilds()
        {
            int idPriceList = 1;
            int idProductionTypeDepartment = 1;
            bool success = false;

            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientBOL newElement = new CowMilkIngredientBOL();
            newElement.DefaultDcperTon = 30;
            newElement.DisplayName = "newDisplayName";
            newElement.IdProductionTypeDepartment = idProductionTypeDepartment;
            newElement.Name = "newName";
            newElement.UnitDisplayName = "newUnitDisplayName";

            ICowMilkIngredientPriceListMonthBOL newElementChild = new CowMilkIngredientPriceListMonthBOL();
            newElementChild.IdCowMilkIngredient = newElement.Id;
            newElementChild.IdCowMilkIngredientPriceList = idPriceList;
            newElementChild.DCPercentage = 20;
            newElementChild.Amount = 50;
            newElementChild.EffectiveMonthDate = DateTime.Now;

            newElement.PriceLists.Add(newElementChild);
            GenericResponse saveResponse = cowMilkBLL.SaveIngredient(newElement);

            if(saveResponse.Succeeded)
            {
                ICowMilkIngredientBOL dbElement = cowMilkBLL.GetIngredient(newElement.Id).Element;
                dbElement.State = CoreLib.Definitions.ObjectState.DELETED;
                dbElement.PriceLists.RemoveAll(x => true);
                saveResponse = cowMilkBLL.SaveIngredient(dbElement);

                dbElement = cowMilkBLL.GetIngredient(newElement.Id).Element;
                ICowMilkIngredientPriceListMonthBOL dbElementChild = cowMilkBLL.GetIngredientPriceListMonth(newElementChild.Id).Element;

                return saveResponse.Succeeded && dbElement.UntypedRecord == null && dbElementChild.UntypedRecord == null;
            }

            return success;
        }
        #endregion

        #region Update
        private bool updateBOLWithoutChilds()
        {
            int idElement = 6;
            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientPriceListMonthBOL elementToUpdate = cowMilkBLL.GetIngredientPriceListMonth(idElement).Element;

            elementToUpdate.Amount += 2;
            GenericResponse saveResponse = cowMilkBLL.SaveIngredientPriceListMonth(elementToUpdate);

            ICowMilkIngredientPriceListMonthBOL dbElement = cowMilkBLL.GetIngredientPriceListMonth(idElement).Element;
            return saveResponse.Succeeded && isEqualElement(elementToUpdate, dbElement);
        }
        private bool updateBOLWithChilds()
        {
            int idElement = 6;
            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            ICowMilkIngredientBOL elementToUpdate = cowMilkBLL.GetIngredient(idElement).Element;

            elementToUpdate.DefaultDcperTon += 10;

            foreach(var childElement in elementToUpdate.PriceLists.GetAllElements())
                childElement.DCPercentage += 1;

            GenericResponse saveResponse = cowMilkBLL.SaveIngredient(elementToUpdate);

            ICowMilkIngredientBOL dbElement = cowMilkBLL.GetIngredient(idElement).Element;
            return saveResponse.Succeeded && isEqualElement(elementToUpdate, dbElement);
        }
        #endregion

        #region Get from another BLL
        private bool getFromAnotherBLL()
        {
            ICowMilkBLL cowMilkBLL = Injector.ImplementBll<ICowMilkBLL>();
            IProductionBLL productionBLL = cowMilkBLL.GetBLL<IProductionBLL>();

            List<IProductionTypeBOL> elements = productionBLL.GetProductionTypes().ElementList;
            return elements != null && elements.Any();
        }
        #endregion

        #region helper methods
        private bool isEqualElement(ICowMilkIngredientBOL source, ICowMilkIngredientBOL target)
        {
            bool isEqual = true;

            isEqual &= source.Id == target.Id;
            isEqual &= source.DefaultDcperTon == target.DefaultDcperTon;
            isEqual &= source.DisplayName == target.DisplayName;
            isEqual &= source.IdProductionTypeDepartment == target.IdProductionTypeDepartment;
            isEqual &= source.Name == target.Name;
            isEqual &= source.UnitDisplayName == target.UnitDisplayName;

            isEqual &= source.PriceLists.Count == target.PriceLists.Count;

            if (isEqual)
            {
                for (int i = 0; i < source.PriceLists.Count; i++)
                    isEqual &= isEqualElement(source.PriceLists[i], target.PriceLists[i]);
            }

            return isEqual;
        }
        private bool isEqualElement(ICowMilkIngredientPriceListMonthBOL source, ICowMilkIngredientPriceListMonthBOL target)
        {
            bool isEqual = true;

            isEqual &= source.Id == target.Id;
            isEqual &= source.IdCowMilkIngredient == target.IdCowMilkIngredient;
            isEqual &= source.IdCowMilkIngredientPriceList == target.IdCowMilkIngredientPriceList;
            isEqual &= source.DCPercentage == target.DCPercentage;
            isEqual &= source.Amount == target.Amount;
            isEqual &= source.EffectiveMonthDate == target.EffectiveMonthDate;
            isEqual &= source.LastModifiedDate == target.LastModifiedDate;
            isEqual &= source.CreatedDate == target.CreatedDate;

            return isEqual;
        }
        #endregion
    }
}
