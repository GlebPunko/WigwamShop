import React from 'react';

import Card from '../../components/Card/Card';
import styles from './Home.module.scss'

function Home({items, searchValue, setSearchValue, onChangeSearchInput,
  onAddToFavorite, onAddToCart, isLoading,
}) {
  const renderItems = () => {
    const filtredItems = items.filter((item) =>
      item.title.toLowerCase().includes(searchValue.toLowerCase()),
    );
    return (isLoading ? [...Array(8)] : filtredItems).map((item, index) => (
      <Card
        key={index} onFavorite={(obj) => onAddToFavorite(obj)}
        onPlus={(obj) => onAddToCart(obj)} loading={isLoading}
        {...item}
      />
    ));
  };

  return (
    <div className={styles.content}>
      <div className={styles.flexContent}>
        <h1>{searchValue ? `Поиск по запросу: "${searchValue}"` : 'Все вигвамы'}</h1>
        <div className={styles.searchBlock}>
          <img src="img/search.svg" alt="Search" />
          {searchValue && (
            <img
              onClick={() => setSearchValue('')}
              className={styles.clear}
              src="img/btn-remove.svg"
              alt="Clear"
            />
          )}
          <input onChange={onChangeSearchInput} value={searchValue} placeholder="Поиск..." />
        </div>
      </div>
      <div className={styles.renderItems}>{renderItems()}</div>
    </div>
  );
}

export default Home;
