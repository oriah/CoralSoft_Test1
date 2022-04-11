import { useState } from "react"
import { Loader } from "./Loader"

export function Item({ item }) {
    const [isLoading, setIsLoading] = useState(true)
    return (
        <>
            {isLoading && <Loader />}
            <div className="item">
                <img src={item.url} onLoad={() => { setIsLoading(false) }} alt="" />
                <h5>{item.title}</h5>
            </div>
        </>
    )
}