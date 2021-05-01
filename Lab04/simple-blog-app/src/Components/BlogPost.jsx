import React, { useState } from 'react'

function BlogPost({ title, authorName, contents }) {

  const [expanded, setExpanded] = useState(false)

  return ( 
    <div>
      <h1>{title}</h1>
      <h4>{authorName}</h4>
      <button onClick={ () => setExpanded(!expanded) } >{expanded ? 'Hide Content' : 'Show Content'}</button>
      { expanded && <p>{contents}</p> }
    </div>
  )
}

export default BlogPost
