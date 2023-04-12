import React from 'react';
import { Route, Switch } from 'react-router-dom';
import axios from 'axios';
import Header from './components/Header/Header';
import Drawer from './components/Drawer/Drawer';
import AppContext from './context';

import Home from './pages/Home/Home';
import Favorites from './pages/Favorites/Favorites';
import Orders from './pages/Orders/Orders';

function App() {
  const [items, setItems] = React.useState([]);
  const [cartItems, setCartItems] = React.useState([]);
  const [favorites, setFavorites] = React.useState([]);
  const [searchValue, setSearchValue] = React.useState('');
  const [cartOpened, setCartOpened] = React.useState(false);
  const [isLoading, setIsLoading] = React.useState(true);

  React.useEffect(() => {
    async function fetchData() {
      try {
        const [cartResponse, favoritesResponse, itemsResponse] = await Promise.all([
          axios.get("https://642b60f0d7081590f92179f8.mockapi.io/cart"),
          axios.get("https://642be4ded7081590f92c7388.mockapi.io/favorites"),
          axios.get("https://642b60f0d7081590f92179f8.mockapi.io/items"),
            //TODO dispatch(getItems())
            //component didmount
        ]);

        setIsLoading(false);
        setCartItems(cartResponse.data);
        setFavorites(favoritesResponse.data);
        setItems(itemsResponse.data);
      } catch (error) {
        alert('Ошибка при запросе данных ;(');
        console.error(error);
      }
    }//TODO Redux посмотреть, диспатч ({type, payload})
    //useSelector для доставания данных

    fetchData();
  }, []);//TODO срабатывает два раза (наверное, возможно рендерится два раза комонент, нужно искать)

  const onAddToCart = async (obj) => {
    try {
      const findItem = cartItems.find((item) => Number(item.parentId) === Number(obj.id));
      if (findItem) {
        setCartItems((prev) => prev.filter((item) => Number(item.parentId) !== Number(obj.id)));
        await axios.delete(`https://642b60f0d7081590f92179f8.mockapi.io/cart/${findItem.id}`);
      } else {
        setCartItems((prev) => [...prev, obj]);
        const { data } = await axios.post('https://642b60f0d7081590f92179f8.mockapi.io/cart/', obj);
        setCartItems((prev) =>
          prev.map((item) => {
            if (item.parentId === data.parentId) {
              return {
                ...item,
                id: data.id,
              };
            }
            return item;
          }),
        );
      }
    } catch (error) {
      alert('Ошибка при добавлении в корзину');
      console.error(error);
    }
  };

  const onAddToFavorite = async (obj) => {
    try {
      const findItem = favorites.find((favObj) => Number(favObj.id) === Number(obj.id));
      if (findItem) {
        console.log("delete");
        console.log(findItem)
        setFavorites((prev) => prev.filter((item) => Number(item.id) !== Number(obj.id)));
        await axios.delete(`https://642be4ded7081590f92c7388.mockapi.io/favorites/${findItem.id}`);
      } else {
        console.log("add");
        console.log(findItem)
        const { data } = await axios.post(
            'https://642be4ded7081590f92c7388.mockapi.io/favorites',
            obj,
        );
        setFavorites((prev) => [...prev, data]);
      }
    } catch (error) {
      alert('Не удалось добавить в фавориты');
      console.error(error);
    }
  };

  const onRemoveItem = (id) => {
    try {
      axios.delete(`https://642b60f0d7081590f92179f8.mockapi.io/cart/${id}`);
      setCartItems((prev) => prev.filter((item) => Number(item.id) !== Number(id)));
    } catch (error) {
      alert('Ошибка при удалении из корзины');
      console.error(error);
    }
  };

  const onChangeSearchInput = (event) => {
    setSearchValue(event.target.value);
  };

  const isItemAdded = (id) => {
    return cartItems.some((obj) => Number(obj.parentId) === Number(id));
  };
  return (
    <AppContext.Provider
      value={{items, cartItems, favorites, isItemAdded, onAddToFavorite,
        onAddToCart, setCartOpened, setCartItems,
      }}>
      <div className="wrapper clear">
        <Drawer items={cartItems} onClose={() => setCartOpened(false)}
          onRemove={onRemoveItem} opened={cartOpened}
        />

        <Header onClickCart={() => setCartOpened(true)} />
        <Switch>
          <Route path="/" exact>
            <Home items={items} cartItems={cartItems} searchValue={searchValue}
                  setSearchValue={setSearchValue} onChangeSearchInput={onChangeSearchInput}
                  onAddToFavorite={onAddToFavorite} onAddToCart={onAddToCart} isLoading={isLoading}/>
          </Route>
          <Route path="/favorites" exact>
            <Favorites />
          </Route>
          <Route path="/orders" exact>
            <Orders />
          </Route>
        </Switch>
      </div>
    </AppContext.Provider>
  );
}

export default App;
