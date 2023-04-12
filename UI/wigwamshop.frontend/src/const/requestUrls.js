export default class RequestUrls {
    static getItems = "https://642b60f0d7081590f92179f8.mockapi.io/items";
    static getCart = "https://642b60f0d7081590f92179f8.mockapi.io/cart";
    static getFavorites = "https://642be4ded7081590f92c7388.mockapi.io/favorites";
    static getOrders = "https://60d62397943aa60017768e77.mockapi.io/orders";
    static postCart = "https://642b60f0d7081590f92179f8.mockapi.io/cart/";
    static postFavorites = "https://642be4ded7081590f92c7388.mockapi.io/favorites";
    static postOrder = "/orders";
    static deleteCart = (id) => `https://642b60f0d7081590f92179f8.mockapi.io/cart/${id}`;
    static deleteFavorites = (id) => `https://642be4ded7081590f92c7388.mockapi.io/favorites/${id}`;
}