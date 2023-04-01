import React from 'react';

const Drawer = () => {
    return (
        <div style={{display: "none"}} className="overlay">
            <div className="drawer">
                <h2 className="d-flex justify-between mb-30">Корзина
                    <img className="removeBtn cu-p" src="/img/btn-remove.svg" alt="Remove" /></h2>
                <div className="items">
                    <div className="cart-item d-flex align-center mb-20">
                        <div style={{backgroundImage: 'url(/img/wigwams/1.jpg)'}} className="cartItemImg">
                        </div>
                        <div className="mr-20 flex">
                            <p className="mb-5">Вигвам 1 с всякими прикольными бирюшечками</p>
                            <b>12 999 р.</b>
                        </div>
                        <img className="removeBtn" src="/img/btn-remove.svg" alt="Remove" />
                    </div>
                    <div className="cart-item d-flex align-center mb-20">
                        <div style={{backgroundImage: 'url(/img/wigwams/1.jpg)'}} className="cartItemImg">   </div>
                        <div className="mr-20 flex">
                            <p className="mb-5">Вигвам 1 с всякими прикольными бирюшечками</p>
                            <b>12 999 р.</b>
                        </div>
                        <img className="removeBtn" src="/img/btn-remove.svg" alt="Remove" />
                    </div>

                </div>
                <div className="cartTotalBlock">
                    <ul>
                        <li className="d-flex">
                            <span>Итого:</span>
                            <div></div>
                            <b>20 000 р.</b>
                        </li>
                        <li className="d-flex">
                            <span>Налог 5%:</span>
                            <div></div>
                            <b>1 000 р.</b>
                        </li>
                    </ul>
                    <button className="greenButton">Оформить заказ<img src="/img/arrow.svg" alt="Arrow" /></button>
                </div>
            </div>
        </div>
    );
};

export default Drawer;