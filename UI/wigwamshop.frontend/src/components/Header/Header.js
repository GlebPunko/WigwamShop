import React from 'react';
import { Link } from 'react-router-dom';

import { useCart } from '../../hooks/useCart';
import ImageUrls from "../../const/inageUrls";
import styles from './Header.module.scss'

function Header(props) {
  const { totalPrice } = useCart();

  return (
    <header className={styles.headerCont}>
      <Link to="/">
        <div className={styles.headerInfo}>
          <img width={40} height={40} src={ImageUrls.logo} alt="Logotype" />
          <div>
            <h3 className={styles.text}>Магазин вигвамов</h3>
            <p className={styles.text}>Магазин лучших вигвамов</p>
          </div>
        </div>
      </Link>
      <ul className={styles.list}>
        <li onClick={props.onClickCart} className={styles.basket}>
          <img width={18} height={18} src={ImageUrls.cart} alt="Корзина" />
          <span>{totalPrice} руб.</span>
        </li>
        <li>
          <Link to="/favorites">
            <img width={18} height={18} src={ImageUrls.heart} alt="Закладки" />
          </Link>
        </li>
        <li>
          <Link to="/orders">
            <img width={18} height={18} src={ImageUrls.user} alt="Пользователь" />
          </Link>
        </li>
      </ul>
    </header>
  );
}

export default Header;
