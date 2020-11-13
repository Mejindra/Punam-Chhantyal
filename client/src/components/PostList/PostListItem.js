import React from 'react';
import { useHistory } from 'react-router-dom';
import slugify from 'slugify';
import './styles.css';

const PostListItem = props => {
    const { post, clickPost, deletePost, editPost } = props;
    const history = useHistory();

    const handleClickPost = post => {
        const slug = slugify(post.tittle, { lower: true });

        clickPost(post);
        history.push(`/posts/${slug}`);
    };

    const handleClickPost = post => {
        editPost(post);
        history.push(`/edit-post/${post.id}`);
    };

    return (
        <div>
            <div className="postListItem" onClick={() => handleClickPost(post)}>
                <h2>{post.title}</h2>
                <p>{post.body}</p>
            </div>
            <div className="postControls">
              <button onClick={() => deletePost(post)}>Delete</button>
              <button onClick={() => handleEditPost(post)}>Edit</button>
            </div>
        </div>
    );
};

export default PostListItem;