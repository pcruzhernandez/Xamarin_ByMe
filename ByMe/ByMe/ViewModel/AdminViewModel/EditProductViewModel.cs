using Acr.UserDialogs;
using ByMe.AdminView;
using ByMe.global;
using ByMe.Model.Response;
using ByMe.Model.UserModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ByMe.ViewModel.AdminViewModel
{
   public class EditProductViewModel:BaseViewModel
    {
            #region property declaration

            private ProductModel product;
            public ProductModel Product
            {
                get { return product; }
                set { product = value; RaisePropertyChanged("Product"); }
            }


            private string name;
            private string type;
            private string itemImage;
            private string desc;
            private string price;
            private string qty;

            public string Type
            {
                get { return type; }
                set { type = value; RaisePropertyChanged("Type"); }
            }

            public string Name
            {
                get { return name; }
                set { name = value; RaisePropertyChanged("Name"); }
            }

            public string ItemImage
            {
                get { return itemImage; }
                set { itemImage = value; RaisePropertyChanged("ItemImage"); }
            }

            public string Desc
            {
                get { return desc; }
                set { desc = value; RaisePropertyChanged("Desc"); }

            }
            public string Price
            {
                get { return price; }
                set { price = value; RaisePropertyChanged("Price"); }

            }
            public string Qty
            {
                get { return qty; }
                set { qty = value; RaisePropertyChanged("Qty"); }

            }
            #endregion

            #region Command
          



            private Command editItemCommand;
            public Command EditItemCommand
            {
                get
                {
                    return editItemCommand ?? (editItemCommand = new Command(() => ExecuteEditItemCommand()));
                }
            }

            #endregion

            private bool CheckValidations()
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    UserDialogs.Instance.Alert("Please enter Name", null, "Cancel");
                    return false;
                }

                else if (string.IsNullOrWhiteSpace(Desc))
                {
                    UserDialogs.Instance.Alert("Please enter Description", null, "Cancel");
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(Price))
                {
                    UserDialogs.Instance.Alert("Please enter Price", null, "Cancel");
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(Qty))
                {
                    UserDialogs.Instance.Alert("Please enter Quantity", null, "Cancel");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            private async void ExecuteEditItemCommand()
            {

                if (CheckValidations())
                {

                    product.Name = Name;
                    product.Price = Convert.ToDouble(Price);
                    product.Qty = Convert.ToInt16(Qty);
                    product.Image = ItemImage;
                    
                    product.Desc = Desc;


                    UserDialogs.Instance.ShowLoading();

                    Rest_Response rest_result = await WebService.PostData(product, "Product/UpdateProduct");
                    if (rest_result != null)
                    {
                        if (rest_result.status_code == 200)
                        {
                            RootObjectProduct data = JsonConvert.DeserializeObject<RootObjectProduct>(rest_result.response_body);
                            if (data.StatusCode == 200)
                            {
                              
                            UserDialogs.Instance.Alert("Item Edited", null, "OK");
                               // App.Current.MainPage = new AdminMasterController();
                            }

                        }
                        else
                        {
                            UserDialogs.Instance.Alert("Product Already Added");
                        }

                    }
                    UserDialogs.Instance.HideLoading();

                }


            }





        }
    }


