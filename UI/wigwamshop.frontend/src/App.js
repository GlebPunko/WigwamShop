import Card from './components/Card/Card';
import Header from './components/Header';
import Drawer from './components/Drawer';
import React, {useState, useEffect} from 'react';

function App() {
    const [items, setItems] = useState([]);
    const [cartItems, setCartItems] = useState([]);
    const [cartOpened, setCartOpened] = useState(false);

    useEffect(() => {
        fetch("https://642b60f0d7081590f92179f8.mockapi.io/items")
            .then((res) => {
                return res.json();
            })
            .then((json) => {
                setItems(json);
            });
    }, [])

    const onAddToCard = (obj) => {
        setCartItems(prev => [...prev, obj]);
    }

    return (
        <div className="wrapper clear">
            {cartOpened && <Drawer items={cartItems} onClose={() => setCartOpened(false)} />}
            <Header inClickCart={() => setCartOpened(true)} />
            <div className="content p-40">
               <div className="d-flex align-center justify-between mb-40">
                   <h1>Все вигвамы</h1>
                   <div className="search-block d-flex">
                       <img src="/img/lupa.svg" alt="search" />
                       <input placeholder="Поиск..."/>
                   </div>
               </div>

                <div className="d-flex flex-wrap">
                    {
                        items.map((item) => (
                            <Card
                                title={item.title}
                                price={item.price}
                                imageUrl={item.imageUrl}
                                onFavorite={() => console.log("добавили в закладки")}
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
