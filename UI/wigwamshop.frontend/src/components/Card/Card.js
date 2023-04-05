import React, {useState} from 'react';
import styles from './Card.module.scss';

const Card = ({ id, title, price, imageUrl , onFavorite, onPlus, favorited= false }) => {
    const [isAdded, setIsAdded] = useState(false);
    const [isFavorite, setIsFavorite] = useState(favorited);

    const onClickPlus = () => {
        onPlus({title, price, imageUrl});
        setIsAdded(!isAdded);
    }

    const onClickFavorite = () => {
        onFavorite({id, title, price, imageUrl});
        setIsFavorite(!isFavorite);
    }

    return (
        <div className={styles.card}>
            <div className={styles.favorite} onClick={onClickFavorite}>
                <img src={isFavorite ? "/img/heard-liked.svg" : "/img/heard-unliked.svg"} alt="Unliked"/>
            </div>
            <img width={133} height={112} src={imageUrl} alt="Wigwams" />
            <h5>{title}</h5>
            <div className="d-flex justify-between align-center">
                <div className="d-flex flex-column">
                    <span>Цена:</span>
                    <b>{price} бел. руб.</b>
                </div>
                <img className={styles.plus}
                     src={isAdded ? "/img/btn-cheked.svg" : "/img/btn-plus.svg"}
                     alt="Plus"
                     onClick={onClickPlus}/>
            </div>
        </div>
    );
};

export default Card;