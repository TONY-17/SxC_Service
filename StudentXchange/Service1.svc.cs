using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentXchange
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService
    {
        DataClasses1DataContext context = new DataClasses1DataContext();
        public int Register(User user)
        {
            var regQuery = context.Users.Where<User>(r => r.Email.Equals(user.Email)).FirstOrDefault();
            
            if(regQuery != null)
            {
                return 0; //Email already exist 
            }

            var RegUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = Secrecy.HashPassword(user.Password)
             
            };

                context.Users.InsertOnSubmit(RegUser);

            try
            {
                context.SubmitChanges();
                return 1; //successfully created
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return -1; //Error something went wrong
            }


        }

        public bool Login(string Email, string Password)
        {
            bool isRegistered = false;

            var userQuery = context.Users.Where<User>(u => u.Email.Equals(Email) &&
                                                   u.Password.Equals(Secrecy.HashPassword(Password))).FirstOrDefault();

            if (userQuery != null)
                isRegistered = true;

            return isRegistered;
        }

        public void closeAcc(string Email, string Password)
        {
            var userQuery = context.Users.Where<User>(u => u.Email.Equals(Email) &&
                                                   u.Password.Equals(Secrecy.HashPassword(Password))).FirstOrDefault();
            context.Users.DeleteOnSubmit(userQuery);
            context.SubmitChanges();
        }

        public void createProduct(Product product, int categoryId,int userId)
        {
            var newproduct = new Product
            {
                Name = product.Name,
                description = product.description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ImageUrlThumbnail1 = product.ImageUrlThumbnail1,
                ImageUrlThumbnail2 = product.ImageUrlThumbnail2,
                ImageUrlThumbnail3 = product.ImageUrlThumbnail3,
                UserId = userId,
                Status = product.Status,
                DateCreated = product.DateCreated,
                CategoryId = categoryId
            };

            context.Products.InsertOnSubmit(newproduct);

            context.SubmitChanges();
        }

        public User getUser(int userId)
        { 
            var user = context.Users.Where<User>(u=>u.Id.Equals(userId)).FirstOrDefault();

            var _user = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Password = user.Password,
                Description = user.Description,
                Email = user.Email
            };
            return _user; //user doesnt exist
        }
        public User getUserbyEmail(string email)
        {
            var user = context.Users.Where<User>(u => u.Email.Equals(email)).FirstOrDefault();

            var _user = new User
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Password = user.Password,
                Description = user.Description,
                Email = user.Email
            };
            return _user; //user doesnt exist
        }
        public List<User> getUsers()
        {
            List<User> listUsers = new List<User>();

            var users = context.Users;
            
            foreach(var u in users)
            {
                listUsers.Add(u);
            }

            return listUsers;
        }

        public Product getProduct(int? productId)
        {
            var product = context.Products.Where<Product>(p => p.Id.Equals(productId)).FirstOrDefault();

                var prod = new Product
                {
                    Name = product.Name,
                    description = product.description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    ImageUrlThumbnail1 = product.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = product.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = product.ImageUrlThumbnail3,
                    UserId = product.UserId,
                    Status = product.Status,
                    DateCreated = product.DateCreated,
                    CategoryId = product.CategoryId
                };
                return prod;
        }

        public List<Product> getProducts()
        {
            List<Product> listProducts= new List<Product>();

            var products = context.Products;

            foreach (var p in  products)
            {
                var prod = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    description = p.description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ImageUrlThumbnail1 = p.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = p.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = p.ImageUrlThumbnail3,
                    UserId = p.UserId,
                    Status = p.Status,
                    DateCreated = p.DateCreated,
                    CategoryId = p.CategoryId
                };
                listProducts.Add(prod);
            }
            return listProducts;
        }

        public Category getCategory(int categoryId)
        {
            var category = context.Categories.Where<Category>(p => p.Id.Equals(categoryId)).FirstOrDefault();

            var cat = new Category
            {
               Id = category.Id,
               Name = category.Name
            };
            return cat;  
        }

        public List<Category> getCategories()
        {
            List<Category> listCategory = new List<Category>();

            var categories = context.Categories.ToList<Category>();

            foreach (var c in categories)
            {
                var category = new Category
                {
                    Id = c.Id,
                    Name = c.Name
                };
                listCategory.Add(category);
            }

            return listCategory;
        }

        public void createCategory(string name)
        {
            var category = new Category
            {
                Name = name
            };
            context.Categories.InsertOnSubmit(category);
            context.SubmitChanges();
        }

        public void createPriceOffer(PriceOffer priceOffer,int prodId)
        {
            var newPriceOffer = new PriceOffer
            {
                Id = priceOffer.Id,
                StartDate = priceOffer.StartDate,
                EndDate = priceOffer.EndDate,
                Status = priceOffer.Status,
                Discount = priceOffer.Discount,
                ProductId = prodId
            };
            context.PriceOffers.InsertOnSubmit(newPriceOffer);
            context.SubmitChanges();
        }

        public void createReview(int stars, string Comment, int productId)
        {
            var review = new Review
            {
                Stars = stars,
                Comment = Comment,
                ProductId = productId
            };
            context.Reviews.InsertOnSubmit(review);
            context.SubmitChanges();
        }

        public List<Product> getUserProducts(int userId)
        {
            var listUserProducts = new List<Product>();

            var userProducts = context.Products.Where<Product>(u => u.UserId.Equals(userId)).ToList();

            foreach(var u in userProducts)
            {
                var uprodcts = new Product
                {
                    Id = u.Id,
                    Name = u.Name,
                    Price = u.Price,
                    DateCreated = u.DateCreated,
                    CategoryId = u.CategoryId,
                    Status = u.Status
                };
                listUserProducts.Add(uprodcts);
            }
            return listUserProducts;
        }

        public void EditProduct(Product product)
        {
            var getProduct = context.Products
                                             .Where<Product>(p=>p.Id.Equals(product.Id))
                                             .FirstOrDefault();

            if (getProduct != null)
            {

                getProduct.Name = product.Name;
                getProduct.Price = product.Price;
               // getProduct.CategoryId = product.CategoryId;
                getProduct.ImageUrl = product.ImageUrl;
                getProduct.ImageUrlThumbnail1 = product.ImageUrlThumbnail1;
                getProduct.ImageUrlThumbnail2 = product.ImageUrlThumbnail2;
                getProduct.ImageUrlThumbnail3 = product.ImageUrlThumbnail3;
              //  getProduct.DateCreated = product.DateCreated;
                getProduct.Status = product.Status;
                
                context.SubmitChanges();

            }
        }

        public void EditUser(User appUser)
        {
            var editUser = context.Users.Where<User>(u => u.Id.Equals(appUser.Id)).FirstOrDefault();

            if (editUser != null)
            {
                editUser.Name = appUser.Name;
                editUser.LastName = appUser.LastName;
                editUser.Password = Secrecy.HashPassword(appUser.Password);
                editUser.Email = appUser.Email;
                editUser.Description = appUser.Description;
            }
            context.SubmitChanges();
        }

        public BillingAddress getUserAddress(int userId)
        {
            var userAddress = context.BillingAddresses.Where<BillingAddress>(b => b.UserId.Equals(userId)).FirstOrDefault();

            if (userAddress != null)
            {
                var _address = new BillingAddress
                {
                    Id = userAddress.Id,
                    Address = userAddress.Address,
                    Address2 = userAddress.Address2,
                    City = userAddress.City,
                    Province = userAddress.Province,
                    PostalCode = userAddress.PostalCode,
                    UserId = userAddress.UserId
                };
                return _address;
            }
            return null;
        }

        public void EditAddress(BillingAddress address)
        {
            var _address = context.BillingAddresses.Where<BillingAddress>(b => b.UserId.Equals(address.UserId)).FirstOrDefault();

            _address.Address = address.Address;
            _address.Address2 = address.Address2;
            _address.City = address.City;
            _address.PostalCode = address.PostalCode;
            _address.Province = address.Province;

            context.SubmitChanges();
        }

        public List<ORDER> getOrders(int userId)
        {
            var listOrders = new List<ORDER>();
            var getUserOrders = context.ORDERs.Where<ORDER>(o => o.UserId.Equals(userId)).ToList();

            foreach(var o in getUserOrders)
            {
                var order = new ORDER
                {
                    Id = o.Id,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    UserId = o.UserId,
                    ProductId = o.ProductId
                };
                listOrders.Add(order);
            }
            return listOrders;
        }

        public List<PriceOffer> getPriceOffers()
        {
            var list = new List<PriceOffer>();
            var getPriceOffers = context.PriceOffers.ToList<PriceOffer>();
           
            foreach(var p in getPriceOffers)
            {
                var _priceoffer = new PriceOffer
                {
                    Id = p.Id,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status,
                    Discount = p.Discount,
                    ProductId = p.ProductId
                };
                list.Add(_priceoffer);
            }
            return list;
        }

        public PriceOffer getPriceOffer(int offerid)
        {
            var getOffer = context.PriceOffers.Where<PriceOffer>(o=>o.Id.Equals(offerid)).FirstOrDefault();

            var _offer = new PriceOffer
            {
                Id = getOffer.Id,
                StartDate = getOffer.StartDate,
                EndDate = getOffer.EndDate,
                Discount = getOffer.Discount,
                Status = getOffer.Status,
                ProductId = getOffer.ProductId
            };
            return _offer;
        }

        public void EditPriceOffer(PriceOffer offer)
        {
           var _offer = context.PriceOffers.Where<PriceOffer>(p=>p.Id.Equals(offer.Id)).FirstOrDefault();

            _offer.Discount = offer.Discount;
            _offer.StartDate = offer.StartDate;
            _offer.EndDate = offer.EndDate;

            context.SubmitChanges();
        }

        public List<Product> getCategoryProducts(int categoryId)
        {
            var getcategoryProducts = context.Products.Where<Product>(c => c.CategoryId.Equals(categoryId));

            var list = new List<Product>();

            foreach(var p in getcategoryProducts)
            {
                var prod = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    description = p.description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ImageUrlThumbnail1 = p.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = p.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = p.ImageUrlThumbnail3,
                    UserId = p.UserId,
                    Status = p.Status,
                    DateCreated = p.DateCreated,
                    CategoryId = p.CategoryId
                };
                list.Add(prod);
            }
          
            return list;
        }

        public void AddToCart(CartItem cartItem,int prodid)
        {
            var _cartItem = new CartItem
            {
                cartId = cartItem.cartId,
                qty = cartItem.qty,
                productId = prodid,
                datecreated = cartItem.datecreated
            };
            context.CartItems.InsertOnSubmit(_cartItem);
            context.SubmitChanges();
        }


        public CartItem getCartItems(string cartId,int prodid)
        {
            var cartItem = context.CartItems.Where<CartItem>(c => c.cartId.Equals(cartId) && c.productId.Equals(prodid)).FirstOrDefault();

            if(cartItem != null)
            {
                var _item = new CartItem
                {
                    ItemId = cartItem.ItemId,
                    productId = cartItem.productId,
                    qty = cartItem.qty,
                    datecreated = cartItem.datecreated,
                    cartId = cartItem.cartId
                };
                return _item;
            }
            return null;
        }

        public List<CartItem> getCartListItems(string cartId, int prodid)
        {
            var cartItems = context.CartItems.Where<CartItem>(c => c.cartId.Equals(cartId));

            var list = new List<CartItem>();

            foreach(var i in cartItems)
            {
                var _item = new CartItem
                {
                    ItemId = i.ItemId,
                    productId = i.productId,
                    qty = i.qty,
                    datecreated = i.datecreated,
                    cartId = i.cartId
                };
                list.Add(_item);
            }

            return list;
        }

        public int cartCount(string cartid)
        {
            var cartItems = context.CartItems.Where<CartItem>(c => c.cartId.Equals(cartid)).Count();
            return cartItems;
        }

        public int saleCount(int prodid,int uid)
        {
            var saleItems = context.ORDERs.Where<ORDER>(c => c.ProductId.Equals(prodid)&& c.UserId.Equals(uid)).Count();
            return saleItems;
        }

        public void deleteCartItem(int cid)
        {
            var cartItem = context.CartItems.Where<CartItem>(c => c.ItemId.Equals(cid)).FirstOrDefault();
            context.CartItems.DeleteOnSubmit(cartItem);
            context.SubmitChanges();
        }

        public void deleteproduct(int id)
        {
            var prod = context.Products.Where<Product>(c => c.Id.Equals(id)).FirstOrDefault();
            context.Products.DeleteOnSubmit(prod);
            context.SubmitChanges();
        }

        public void deleteorder(int id)
        {
            var prod = context.ORDERs.Where<ORDER>(c => c.Id.Equals(id)).FirstOrDefault();
            context.ORDERs.DeleteOnSubmit(prod);
            context.SubmitChanges();
        }

        public void deletepriceoffer(int id)
        {
            var prod = context.PriceOffers.Where<PriceOffer>(c => c.Id.Equals(id)).FirstOrDefault();
            context.PriceOffers.DeleteOnSubmit(prod);
            context.SubmitChanges();
        }

        public void createOrder(string cartid)
        {
            var cartItems = context.CartItems.Where<CartItem>(c => c.cartId.Equals(cartid));

            foreach (var i in cartItems)
            {
                var itemexist = context.ORDERs.Where<ORDER>(o => o.ProductId.Equals(i.productId)).FirstOrDefault();
                
                if(itemexist == null)
                {
                    var _item = new ORDER
                    {
                        // Price = price,
                        Quantity = i.qty,
                        UserId = Convert.ToInt32(i.cartId),
                        ProductId = i.productId
                    };
                    context.ORDERs.InsertOnSubmit(_item);
                    context.SubmitChanges();
                }
             
            }

        }

        public void CreateUserAddress(BillingAddress billingAddress, int userId)
        {
            var newAddress = new BillingAddress
            {
                Address = billingAddress.Address,
                Address2 = billingAddress.Address2,
                City = billingAddress.City,
                Province = billingAddress.Province,
                PostalCode = billingAddress.PostalCode,
                UserId = userId
            };
            context.BillingAddresses.InsertOnSubmit(newAddress);
            context.SubmitChanges();
        }

        public List<Product> getCategoryProductsHighlow(int categoryId)
        {
            var getcategoryProducts = context.Products.Where<Product>(c => c.CategoryId.Equals(categoryId)).OrderBy(p=>p.Price);

            var list = new List<Product>();

            foreach (var p in getcategoryProducts)
            {
                var prod = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    description = p.description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ImageUrlThumbnail1 = p.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = p.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = p.ImageUrlThumbnail3,
                    UserId = p.UserId,
                    Status = p.Status,
                    DateCreated = p.DateCreated,
                    CategoryId = p.CategoryId
                };
                list.Add(prod);
            }

            return list;
        }

        public List<Product> getCategoryProductsLowhigh(int categoryId)
        {
            var getcategoryProducts = context.Products.Where<Product>(c => c.CategoryId.Equals(categoryId)).OrderByDescending(p => p.Price);

            var list = new List<Product>();

            foreach (var p in getcategoryProducts)
            {
                var prod = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    description = p.description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ImageUrlThumbnail1 = p.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = p.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = p.ImageUrlThumbnail3,
                    UserId = p.UserId,
                    Status = p.Status,
                    DateCreated = p.DateCreated,
                    CategoryId = p.CategoryId
                };
                list.Add(prod);
            }

            return list;
        }

        public void updateQty(string cartid, int prodid, int quantity)
        {
            var updateQty = context.CartItems.Where<CartItem>(u => u.cartId.Equals(cartid) && u.productId.Equals(prodid)).FirstOrDefault();

            updateQty.qty = quantity;

            context.SubmitChanges();
        }

        public void updateOrderIsBought(int userid, int prodid)
        {
            var updateQty = context.ORDERs.Where<ORDER>(o => o.UserId.Equals(userid) && o.ProductId.Equals(prodid)).FirstOrDefault();
     
            updateQty.Isbought = 1;

            context.SubmitChanges();
        }

        public List<ORDER> getOrdersUserIsBought(int userId)
        {
            var listOrders = new List<ORDER>();
            var getUserOrders = context.ORDERs.Where<ORDER>(o => o.UserId.Equals(userId) && o.Isbought.Equals(1)).ToList();

            if (getUserOrders != null)
            {
                foreach (var o in getUserOrders)
                {
                    var order = new ORDER
                    {
                        Id = o.Id,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        UserId = o.UserId,
                        ProductId = o.ProductId
                    };
                    listOrders.Add(order);
                }
                return listOrders;
            }
            return null;
        }

        public void updateOrderIsAlreadyBought(int userid, int prodid)
        {
            var updateQty = context.ORDERs.Where<ORDER>(o => o.UserId.Equals(userid) && o.ProductId.Equals(prodid)).FirstOrDefault();

            updateQty.Isbought = 0;

            context.SubmitChanges();
        }

        public List<Product> searchresults(string searchvalue)
        {
            var getsearchProducts = context.Products.Where<Product>(r => r.Name.Contains(searchvalue) || r.description.Contains(searchvalue)).ToList();

            var list = new List<Product>();

            foreach (var p in getsearchProducts)
            {
                var prod = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    description = p.description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ImageUrlThumbnail1 = p.ImageUrlThumbnail1,
                    ImageUrlThumbnail2 = p.ImageUrlThumbnail2,
                    ImageUrlThumbnail3 = p.ImageUrlThumbnail3,
                    UserId = p.UserId,
                    Status = p.Status,
                    DateCreated = p.DateCreated,
                    CategoryId = p.CategoryId
                };
                list.Add(prod);
            }

            return list;
        }

    }
}
