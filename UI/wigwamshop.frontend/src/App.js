function App() {
    return (
        <div className="wrapper clear">
            <header className="d-flex justify-between align-center p-40">
                <div className="d-flex align-center">
                    <img width={40} height={40} src="/img/Logo.png" />
                    <div>
                        <h3 className="text-uppercase">React Sneakers</h3>
                        <p className="opacity-5">Магазин вигвамов для детей</p>
                    </div>
                </div>
                <ul className="d-flex">
                    <li className="mr-30">
                        <img width={18} height={18} src="/img/basket.svg" />
                        <span>1205 руб.</span>
                    </li>
                    <li>
                        <img width={18} height={18} src="/img/user.svg" />
                    </li>
                </ul>
            </header>
            <div className="content p-40">
                <h1 className="mb-40">Все вигвамы</h1>

                <div className="d-flex">
                    <div className="card">
                        <img width={133} height={112} src="/img/wigwams/1.jpg" alt="Sneakers" />
                        <h5>Вигвам 1</h5>
                        <div className="d-flex justify-between align-center">
                            <div className="d-flex flex-column">
                                <span>Цена:</span>
                                <b>12 999 руб.</b>
                            </div>
                            <button className="button">
                                <img width={11} height={11} src="/img/plus.svg" alt="Plus" />
                            </button>
                        </div>
                    </div>
                    <div className="card">
                        <img width={133} height={112} src="/img/wigwams/2.jpg" alt="Sneakers" />
                        <h5>Вигвам 2</h5>
                        <div className="d-flex justify-between align-center">
                            <div className="d-flex flex-column">
                                <span>Цена:</span>
                                <b>12 999 руб.</b>
                            </div>
                            <button className="button">
                                <img width={11} height={11} src="/img/plus.svg" alt="Plus" />
                            </button>
                        </div>
                    </div>
                    <div className="card">
                        <img width={133} height={112} src="/img/wigwams/3.jpg" alt="Sneakers" />
                        <h5>Вигвам 3</h5>
                        <div className="d-flex justify-between align-center">
                            <div className="d-flex flex-column">
                                <span>Цена:</span>
                                <b>12 999 руб.</b>
                            </div>
                            <button className="button">
                                <img width={11} height={11} src="/img/plus.svg" alt="Plus" />
                            </button>
                        </div>
                    </div>
                    <div className="card">
                        <img width={133} height={112} src="/img/wigwams/4.jpg" alt="Sneakers" />
                        <h5>Вигвам 4</h5>
                        <div className="d-flex justify-between align-center">
                            <div className="d-flex flex-column">
                                <span>Цена:</span>
                                <b>12 999 руб.</b>
                            </div>
                            <button className="button">
                                <img width={11} height={11} src="/img/plus.svg" alt="Plus" />
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;
