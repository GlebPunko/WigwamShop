import React, {useContext} from 'react';
import AppContext from '../../context';
import styles from './Info.module.scss'

const Info = ({ title, image, description }) => {
  const { setCartOpened } = useContext(AppContext);

  return (
    <div className={styles.cartEmpty}>
      <img className={styles.imageEmpty} width="120px" src={image} alt="Empty" />
      <h2>{title}</h2>
      <p className={styles.description}>{description}</p>
      <button onClick={() => setCartOpened(false)} className={styles.greenButton}>
        <img src="img/arrow.svg" alt="Arrow" />
        Вернуться назад
      </button>
    </div>
  );
};

export default Info;
