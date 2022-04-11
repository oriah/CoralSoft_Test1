import { Item } from "./Item";
import InfiniteScroll from 'react-infinite-scroller';

export function ItemsList({ items, fetchMoreData }) {
    return (
        <InfiniteScroll
            pageStart={0}
            loadMore={fetchMoreData}
            className="items-list grid"
            hasMore={true}
            loader={<h4 key="loader">Loading...</h4>}
        >
            {items.map((item, index) => <Item item={item} key={index}></Item>)}
        </InfiniteScroll>
    )
}