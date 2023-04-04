import React from 'react';

const Drawer = ({onClose, onRemove, items=[] }) => {
    console.log(items)
    return (
        <div className="overlay">
            <div className="drawer">
                <h2 className="d-flex justify-between mb-30">
                    Корзина <img className="removeBtn cu-p" src="/img/btn-remove.svg" alt="Close" onClick={onClose} />
                </h2>
                {
                    items.length > 0 ? (<div className="items">
                        {
                            items.map((obj) => (
                                <div>
                                    <div className="cart-item d-flex align-center mb-20">
                                        <div style={{backgroundImage: `url(${obj.imageUrl})`}}
                                             className="cartItemImg">
                                        </div>
                                        <div className="mr-20 flex">
                                            <p className="mb-5">{obj.title}</p>
                                            <b>{obj.price} бел. р.</b>
                                        </div>
                                        <img className="removeBtn"
                                             src="/img/btn-remove.svg"
                                             alt="Remove"
                                             onClick={() => onRemove(obj.id)} />
                                    </div>
                                </div>
                            ))}
                        <div className="cartTotalBlock">
                            <ul>
                                <li className="d-flex">
                                    <span>Итого:</span>
                                    <div></div>
                                    <b>20 000 бел. р.</b>
                                </li>
                                <li className="d-flex">
                                    <span>Налог 5%:</span>
                                    <div></div>
                                    <b>1 000 бел. р.</b>
                                </li>
                            </ul>
                            <button className="greenButton">Оформить заказ<img src="/img/arrow.svg" alt="Arrow" /></button>
                        </div>
                    </div> ) : (
                        <div className="cartEmpty d-flex align-center justify-center flex-column flex">
                            <img className="mb-20" width="120px" height="120px" src="/img/empty-cart.jpg" alt="Empty"/>
                            <h2>Корзина пустая</h2>
                            <p className="opacity-6">Добавьте хотя бы одну пару кроссовок, чтобы сделать заказ.</p>
                            <button onClick={onClose} className="greenButton">
                                <img src="/img/arrow.svg" alt="Arrow"/>
                                Вернуться назад
                            </button>
                        </div> )

                }
            </div>
        </div>
    );
};

export default Drawer;