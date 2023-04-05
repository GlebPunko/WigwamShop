import Header from './components/Header';
import Drawer from './components/Drawer';
import React, {useState, useEffect} from 'react';
import axios from "axios";
import {Route, Switch} from 'react-router-dom';
import Home from './pages/Home'
import Favorites from "./pages/Favorites";

function App() {
    const [items, setItems] = useState([]);
    const [cartItems, setCartItems] = useState([]);
    const [searchValue, setSearchValue] = useState('');
    const [cartOpened, setCartOpened] = useState(false);
    const [favorites, setFavorites] = useState([]);

    useEffect(() => {
        axios.get("https://642b60f0d7081590f92179f8.mockapi.io/items")
            .then(res => {
                setItems(res.data);
            });
        axios.get("https://642b60f0d7081590f92179f8.mockapi.io/cart")
            .then(res => {
                setCartItems(res.data);
            });
        axios.get("https://642be4ded7081590f92c7388.mockapi.io/favorites")
            .then(res => {
                setFavorites(res.data);
            });
    }, []);

    const onAddToCard = (obj) => {
        axios.post("https://642b60f0d7081590f92179f8.mockapi.io/cart", obj);
        setCartItems(prev => [...prev, obj]);
    }

    const onRemoveItem = (id) => {
        axios.delete(`https://642b60f0d7081590f92179f8.mockapi.io/cart/${id}`);
        setCartItems(prev => prev.filter(item => item.id !== id ));
    }

    const onAddToFavorite = async (obj) => {
        try {
            if(favorites.find(favObj => favObj.id === obj.id)) {
                axios.delete(`https://642be4ded7081590f92c7388.mockapi.io/favorites/${obj.id}`)
                //setFavorites(prev => prev.filter(item => item.id !== obj.id ))
            }
            else {
                const { data } = await axios.post(`https://642be4ded7081590f92c7388.mockapi.io/favorites`, obj);
                setFavorites((prev) => [...prev, data]);
            }
        }
        catch (error) {
            alert("Не удалось добавить в избранное.")
        }
    }

    const onChangeSearchInput = (event) => {
        setSearchValue(event.target.value);
    }

    return (
        <div className="wrapper clear">
            {cartOpened && <Drawer items={cartItems} onRemove={onRemoveItem} onClose={() => setCartOpened(false)} />}
            <Header inClickCart={() => setCartOpened(true)} />
            <Switch>
                <Route path="/" exact>
                    <Home items={items}
                          searchValue={searchValue}
                          setSearchValue={setSearchValue}
                          onChangeSearchInput={onChangeSearchInput}
                          onAddToFavorite={onAddToFavorite}
                          onAddToCard={onAddToCard}/>
                </Route>
                <Route path="/favorites">
                    <Favorites items={favorites} onAddToFavorite={onAddToFavorite}/>
                </Route>
            </Switch>

        </div>
    );
}

export default App;
