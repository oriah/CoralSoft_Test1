import { useState, useEffect, useMemo } from 'react';
import { apiService } from './services/api.service';
import { ItemsList } from './cmps/ItemsList';
import { SortBy } from './cmps/SortBy';
import { Filter } from './cmps/Filter';
import './assets/styles/style.scss';

function App() {
  const ITEMS_PER_PAGE = 12;
  const [currIndex, setCurrIndex] = useState(0);
  const [items, setItems] = useState([]);
  const [criteria, setCriteria] = useState({
    sortBy: 'Sort By',
    filter: ''
  });

  useEffect(() => {
    fetchMoreData();
  }, [])

  const fetchMoreData = async () => {
    try {
      const data = await apiService.get(ITEMS_PER_PAGE, currIndex);
      setCurrIndex(currIndex + ITEMS_PER_PAGE);
      setItems((prevState) => ([
        ...prevState,
        ...data
      ]));
    } catch (e) {
      console.log(e);
    }
  }

  const setSortBy = (sortBy) => {
    const newCriteria = { ...criteria };
    newCriteria.sortBy = sortBy;
    setCriteria(newCriteria);
  }

  const setFilter = (q) => {
    const newCriteria = { ...criteria };
    newCriteria.filter = q;
    setCriteria(newCriteria);
  }

  const getItemsUsingCriteria = useMemo(() => () => {
    let data = [...items];
    if (criteria.sortBy === 'ASC') {
      data = data.sort((a, b) => {
        if (a.title < b.title) { return -1; }
        if (a.title > b.title) { return 1; }
        return 0;
      });
    } else if (criteria.sortBy === 'DESC') {
      data = data.sort((a, b) => {
        if (a.title < b.title) { return 1; }
        if (a.title > b.title) { return -1; }
        return 0;
      });
    }

    if (criteria.filter.trim() !== '') {
      data = data.filter(item => item.title.toLowerCase().startsWith(criteria.filter.toLowerCase()));
    }

    return data;
  }, [criteria, items]);

  return (
    <div className="App main-layout">
      {items.length === 0 && (
        <div>Loading...</div>
      )}
      {items.length > 0 && (
        <>
          <div className="top flex">
            <Filter q={criteria.filter} onChange={setFilter}></Filter>
            <SortBy sortBy={criteria.sortBy} onChange={setSortBy}></SortBy>
          </div>
          <ItemsList items={getItemsUsingCriteria()} fetchMoreData={fetchMoreData}></ItemsList>
        </>
      )}
    </div>
  );
}

export default App;
