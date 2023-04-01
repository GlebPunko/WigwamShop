import Card from './components/Card/Card';
import Header from './components/Header';
import Drawer from './components/Drawer';

const arr = [
    {
        name : 'Вигвам 1, классический',
        price : 300,
        imageUrl : "/img/wigwams/1.jpg"
    },
    {
        name : 'Вигвам 2, модернизированный',
        price : 350,
        imageUrl : "/img/wigwams/2.jpg"
    },
    {
        name : 'Вигвам 3, модернфиолет дизайн',
        price : 400,
        imageUrl : "/img/wigwams/3.jpg"
    }
];

function App() {
    return (
        <div className="wrapper clear">
            <Drawer />

            <Header />

            <div className="content p-40">
               <div className="d-flex align-center justify-between mb-40">
                   <h1>Все вигвамы</h1>
                   <div className="search-block d-flex">
                       <img src="/img/lupa.svg" alt="search" />
                       <input placeholder="Поиск..."/>
                   </div>
               </div>

                <div className="d-flex">
                    {
                        arr.map((obj) => (
                            <Card title={obj.name} price={obj.price} imageUrl={obj.imageUrl} />
                        ))
                    }
                </div>
            </div>
        </div>
    );
}

export default App;
