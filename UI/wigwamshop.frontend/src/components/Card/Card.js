import React from 'react';
import styles from './Card.module.scss';

const Card = (props) => {
    return (
        <div className={styles.card}>
            <div className={styles.favorite}>
                <img src="/img/heard-unliked.svg" alt="Unliked"/>
            </div>
            <img width={133} height={112} src={props.imageUrl} alt="Wigwams" />
            <h5>{props.title}</h5>
            <div className="d-flex justify-between align-center">
                <div className="d-flex flex-column">
                    <span>Цена:</span>
                    <b>{props.price} бел. руб.</b>
                </div>
                <button className="button">
                    <img width={11} height={11} src="/img/plus.svg" alt="Plus" />
                </button>
            </div>
        </div>
    );
};

export default Card;