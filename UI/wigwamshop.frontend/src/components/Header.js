import React from 'react';
import {Link} from 'react-router-dom';

const Header = (props) => {
    return (
        <header className="d-flex justify-between align-center p-40">
            <Link to="/">
                <div className="d-flex align-center">
                    <img width={40} height={40} src="/img/Logo.png" alt="LogoImg"/>
                    <div>
                        <h3 className="text-uppercase">React Sneakers</h3>
                        <p className="opacity-5">Магазин вигвамов для детей</p>
                    </div>
                </div>
            </Link>
            <ul className="d-flex">
                <li onClick={props.inClickCart} className="mr-30 cu-p">
                    <img width={18} height={18} src="/img/basket.svg" alt="BasketImg"/>
                    <span>1205 руб.</span>
                </li>
                <li className="cu-p">
                    <Link to="/favorites">
                        <img width={18} height={18} src="/img/heard.svg" alt="Notes"/>
                    </Link>
                </li>
                <li>
                    <img width={18} height={18} src="/img/user.svg" alt="UserImg"/>
                </li>
            </ul>
        </header>
    );
};

export default Header;