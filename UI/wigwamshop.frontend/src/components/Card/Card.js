import React, {useState, useContext} from 'react';
import ContentLoader from 'react-content-loader';

import AppContext from '../../context';

import styles from './Card.module.scss';
import ImageUrls from "../../const/inageUrls";
import ContentLoaderHelper from "../../helpers/ContentLoaderHelper";

function Card({
  id,
  title,
  imageUrl,
  price,
  onFavorite,
  onPlus,
  favorited = false,
  loading = false,
}) {
  const { isItemAdded } = useContext(AppContext);
  const [isFavorite, setIsFavorite] = useState(favorited);
  const obj = { id, parentId: id, title, imageUrl, price };

  const onClickPlus = () => onPlus(obj);

  const onClickFavorite = () => {
    console.log(obj)
    onFavorite(obj);
    setIsFavorite(!isFavorite);
  };

  return (
    <div className={styles.card}>
      {loading ? (
        <ContentLoaderHelper />
      ) : (
        <>
          {onFavorite && (
            <div className={styles.favorite} onClick={onClickFavorite}>
              <img src={isFavorite ? ImageUrls.likedSvg : ImageUrls.unLikedSvg} alt="Unliked" />
            </div>
          )}
          <img width="100%" height={135} src={imageUrl} alt="Wigwams" />
          <h5>{title}</h5>
          <div className={styles.afterTitle}>
            <div className={styles.price}>
              <span>Цена:</span>
              <b>{price} руб.</b>
            </div>
            {onPlus && (
              <img
                className={styles.plus}
                onClick={onClickPlus}
                src={isItemAdded(id) ? ImageUrls.buttonChecked : ImageUrls.buttonPlus}
                alt="Plus"
              />
            )}
          </div>
        </>
      )}
    </div>
  );
}

export default Card;
