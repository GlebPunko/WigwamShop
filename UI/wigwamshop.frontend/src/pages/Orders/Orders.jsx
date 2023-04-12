import React, {useState, useContext} from 'react';
import axios from 'axios';

import Card from '../../components/Card/Card';
import AppContext from '../../context';
import RequestUrls from "../../const/requestUrls";

function Orders() {
  const { onAddToFavorite, onAddToCart } = useContext(AppContext);
  const [orders, setOrders] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  React.useEffect(() => {
    (async () => {
      try {
        const { data } = await axios.get(RequestUrls.getOrders);//TODO будет чуть позже, но должно работать, наверное, делал как и сотальные запросы
        setOrders(data.reduce((prev, obj) => [...prev, ...obj.items], []));
        setIsLoading(false);
      } catch (error) {
        alert('Ошибка при запросе заказов');
        console.error(error);
      }
    })();
  }, []);

  return (
    <div className="content p-40">
      <div className="d-flex align-center justify-between mb-40">
        <h1>Мои заказы</h1>
      </div>

      <div className="d-flex flex-wrap">
        {(isLoading ? [...Array(8)] : orders).map((item, index) => (
          <Card key={index} loading={isLoading} {...item} />
        ))}
      </div>
    </div>
  );
}

export default Orders;
