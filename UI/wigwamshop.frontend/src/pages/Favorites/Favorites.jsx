import React from 'react';
import Card from '../../components/Card/Card';
import AppContext from '../../context';
import styles from './Favorites.module.scss';

function Favorites() {
  const { favorites, onAddToFavorite } = React.useContext(AppContext);

  return (
    <div className={styles.content}>
      <div className={styles.contentFlex}>
        <h1>Мои закладки</h1>
      </div>

      <div className={styles.cards}>
        {favorites.map((item, index) => (
          <Card key={index} favorited={true} onFavorite={onAddToFavorite} {...item} />
        ))}
      </div>
    </div>
  );
}

export default Favorites;
