using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentXchange
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        int Register(User user);

        [OperationContract]
        bool Login(string Email, string Password);

        [OperationContract]
        void closeAcc(string Email, string Password);

        [OperationContract]
        void createProduct(Product product,int categoryId, int userId);

        [OperationContract]
        User getUser(int userId);

        [OperationContract]
        List<User> getUsers();

        [OperationContract]
        Product getProduct(int? productId);

        [OperationContract]
        List<Product> getProducts();

        [OperationContract]
        List<Product> getUserProducts(int userId);

        [OperationContract]
        List<Product> getCategoryProducts(int categoryId);

        [OperationContract]
        List<Product> getCategoryProductsHighlow(int categoryId);

        [OperationContract]
        List<Product> getCategoryProductsLowhigh(int categoryId);

        [OperationContract]
        Category getCategory(int categoryId);

        [OperationContract]
        List<Category> getCategories();

        [OperationContract]
        void createCategory(string name);

        [OperationContract]
        void createPriceOffer(PriceOffer priceOffer,int prodId);

        [OperationContract]
        void createReview(int stars,string Comment,int productId);

        [OperationContract]
        void EditProduct(Product product);

        [OperationContract]
        void EditUser(User appUser);

        [OperationContract]
        BillingAddress getUserAddress(int userId);

        [OperationContract]
        void CreateUserAddress(BillingAddress billingAddress,int userId);

        [OperationContract]
        void EditAddress(BillingAddress address);

        [OperationContract]
        List<ORDER> getOrders(int userId);

        [OperationContract]
        void createOrder(string cartid);

        [OperationContract]
        List<PriceOffer> getPriceOffers();

        [OperationContract]
        PriceOffer getPriceOffer(int offerid);

        [OperationContract]
        void EditPriceOffer(PriceOffer offer);

        [OperationContract]
        void AddToCart(CartItem cartItem,int prodid);

        [OperationContract]
        CartItem getCartItems(string cartId,int prodid);

        [OperationContract]
        List<CartItem> getCartListItems(string cartId, int prodid);

        [OperationContract]
        int cartCount(string cartid);

        [OperationContract]
        int saleCount(int prodid,int uid);

        [OperationContract]
        User getUserbyEmail(string email);

        [OperationContract]
        void deleteCartItem(int cid);

        [OperationContract]
        void deleteorder(int id);

        [OperationContract]
        void deletepriceoffer(int id);

        [OperationContract]
        void deleteproduct(int id);
        //[OperationContract]
        //void order(int UserId);

        [OperationContract]
        List<ORDER> getOrdersUserIsBought(int userId);

        [OperationContract]
        void updateOrderIsAlreadyBought(int userid, int prodid);

        [OperationContract]
        void updateQty(string cartid, int prodid, int quantity);

        [OperationContract]
        void updateOrderIsBought(int userid, int prodid);

        [OperationContract]
        List<Product> searchresults(string searchvalue);
    }
}
