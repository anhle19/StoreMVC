using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace StoreMVC.Helpers
{
    public static class CartHelper
    {
        private const string CartSessionKey = "Cart";

        public static List<CartItem> GetCart(HttpSessionStateBase session)
        {
            var cart = session[CartSessionKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                session[CartSessionKey] = cart;
            }
            return cart;
        }

        public static void SaveCart(HttpSessionStateBase session, List<CartItem> cart)
        {
            session[CartSessionKey] = cart;
        }

        public static void AddToCart(HttpSessionStateBase session, CartItem item)
        {
            var cart = GetCart(session);
            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);

            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

        }

        public static void UpdateQuantity(HttpSessionStateBase session, int productId, int quantity)
        {
            var cart = GetCart(session);
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    // Nếu số lượng <= 0 thì xóa luôn sản phẩm
                    cart.Remove(item);
                }
                SaveCart(session, cart);
            }
        }

        public static void RemoveFromCart(HttpSessionStateBase session, int productId)
        {
            var cart = GetCart(session);
            var existing = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existing != null)
            {
                cart.Remove(existing);
            }
        }

        public static void ClearCart(HttpSessionStateBase session)
        {
            session[CartSessionKey] = new List<CartItem>();
        }

        public static decimal GetTotal(HttpSessionStateBase session)
        {
            var cart = GetCart(session);
            return cart.Sum(c => c.Total);
        }
        
        public static int GetCartCount(HttpSessionStateBase session)
        {
            var cart = GetCart(session);
            return cart.Count();
        }
    }
}