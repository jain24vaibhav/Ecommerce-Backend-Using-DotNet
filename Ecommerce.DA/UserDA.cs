using Ecommerce.models;
using Ecommerce.models.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecommerce.DA
{
    public class UserDA
    {
        EcommerceContext _db = new EcommerceContext();

        public ICollection<User> GetAllUsers()
        {
            var res = _db.user.ToArray();
            return res;
        }

        public User GetUser(string email)
        {
            var res = _db.user.Where(z => z.userEmail == email).FirstOrDefault();
            return res;
        }

        public bool IsUserByEmail(string email)
        {
            var res = _db.user.Where(x => x.userEmail == email).FirstOrDefault();
            if (res != null)
                return true;
            else
                return false;
        }

        public User UserLogin(LoginModel user)
        {
            var isUser = _db.user.Where(x => x.userEmail == user.userEmail && x.userPassword == user.userPassword).FirstOrDefault();
            if (isUser != null)
                return isUser;
            else
                return null;
    
        }

        public bool UserRegister(User user)
        {
            try
            {
                _db.user.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddToCart(Cart cart)
        {
            var res = _db.cart.Add(cart);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsInCart(Cart cart)
        {
            var res = _db.cart.Where(x => x.productId == cart.productId && x.userId == cart.userId).FirstOrDefault();
            if (res != null)
                return true;
            else
                return false;
        }


        public bool RemoveFromCart(Cart cart)
        {
            var inCart = _db.cart.Where(x => x.productId == cart.productId && x.userId == cart.userId).FirstOrDefault();
            if (inCart != null)
            {
                try
                {
                    _db.cart.Remove(inCart);
                    _db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
               
            else
                return false;
        }

        public bool RemoveAllItemsOfUserFromCart(int userId)
        {
            var items = _db.cart.Where(x => x.userId == userId).ToArray() ;
            foreach(var i in items)
            {
                _db.cart.Remove(i);
                try
                {
                    _db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


        public ICollection<Product> GetCartItemsOfUser(int userId)
        {
            var res = (from crt in _db.cart
                       join pro in _db.products on crt.productId equals pro.productId
                       where crt.userId == userId
                       select pro).ToArray();
            return res;

        }

        public int GenerateOrderId(order ordr)
        {
            try
            {
                _db.order.Add(ordr);
                _db.SaveChanges();
                var generatedOrderId = _db.order.Where(x => x.userId == ordr.userId).Select(x=>x.orderId).LastOrDefault();
                return generatedOrderId;
            }
            catch
            {
                return -1;
            }
        }

        public bool AddToOrderDetail(OrderDetail od)
        {
            _db.orderDetail.Add(od);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddAddress(Address adrs)
        {
            var res = _db.addresse.Add(adrs);
            _db.SaveChanges();
            try
            {
                
                return true;
            }
            catch
            {
                return false;
            }

        }


        public ICollection<object> GetOrderHistory(int id)
        {
            var res = (from o in _db.order
                       join od in _db.orderDetail on o.orderId equals od.orderId
                       join pro in _db.products on od.productId equals pro.productId
                       join adrs in _db.addresse on o.orderId equals adrs.orderId
                       where o.userId == id
                       select new
                       {
                           o.orderId,
                           pro.productName,
                           pro.productPrice,
                           pro.productImage,
                           adrs.area,
                           o.orderDate,
                           adrs.name,
                           adrs.houseno,
                           adrs.city,
                           adrs.state,
                           adrs.pincode
                       }).
                       OrderByDescending(x=>x.orderId).ToArray() ;
            return res;

        }

    }
}
