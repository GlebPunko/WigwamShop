import Card from "../components/Card/Card";

function Home({items, searchValue, setSearchValue, onChangeSearchInput, onAddToFavorite, onAddToCard}) {
    return (
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
                                id={item.id}
                                title={item.title}
                                price={item.price}
                                imageUrl={item.imageUrl}
                                onFavorite={(obj) => onAddToFavorite(obj)}
                                onPlus={(obj) => onAddToCard(obj)}
                            />
                        ))
                }
            </div>
        </div>
    );
}

export default Home;