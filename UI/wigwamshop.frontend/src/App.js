import Card from './components/Card/Card';
import Header from './components/Header';
import Drawer from './components/Drawer';
import React, {useState, useEffect} from 'react';
import axios from "axios";

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
    }, []);

    const onAddToCard = (obj) => {
        axios.post("https://642b60f0d7081590f92179f8.mockapi.io/cart", obj);
        setCartItems(prev => [...prev, obj]);
    }

    const onRemoveItem = (id) => {
        axios.delete(`https://642b60f0d7081590f92179f8.mockapi.io/cart/${id}`);
        setCartItems(prev => prev.filter(item => item.id !== id ));
    }

    const onFavoriteAddToFavorite = (obj) => {
        console.log("add to favorites");
        axios.post(`https://642be4ded7081590f92c7388.mockapi.io/favorites`, obj);
        setFavorites((prev) => [...prev, obj]);
    }

    const onChangeSearchInput = (event) => {
        setSearchValue(event.target.value);
    }

    return (
        <div className="wrapper clear">
            {cartOpened && <Drawer items={cartItems} onRemove={onRemoveItem} onClose={() => setCartOpened(false)} />}
            <Header inClickCart={() => setCartOpened(true)} />
            <div className="content p-40">
               <div className="d-flex align-center justify-between mb-40">
                   <h1>{searchValue ? `Поиск по запросу: ${searchValue}` : 'Все вигвамы'}</h1>
                   <div className="search-block d-flex">
                       <img src="/img/lupa.svg" alt="search" />
                       <input placeholder="Поиск..." value={searchValue} onChange={onChangeSearchInput}/>
                   </div>
               </div>

                <div className="d-flex flex-wrap">
                    {
                        items.filter(item => item.title.toLowerCase().includes(searchValue.toLowerCase()))
                            .map((item, index) => (
                            <Card
                                key={index}
                                title={item.title}
                                price={item.price}
                                imageUrl={item.imageUrl}
                                onFavorite={(obj) => onFavoriteAddToFavorite(obj)}
                                onPlus={(obj) => onAddToCard(obj)}
                            />
                        ))
                    }
                </div>
            </div>
        </div>
    );
}

export default App;
