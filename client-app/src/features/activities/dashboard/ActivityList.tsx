import React from 'react';
import { Header, Item } from 'semantic-ui-react';
import ActivityListItem from './ActivityListItem';
import { Fragment } from 'react';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';




export default observer(function ActivityList() {
    const {activityStore} = useStore();
    const {groupedActivities} = activityStore;
    // not sure why removing Item Group causes an error, stergios 9/22
    return (
        <>
        {groupedActivities.map(([group, activities]) => (
            <Fragment key={group}>
                <Header sub color='teal'>
                    {group}
                </Header>
        
                    <Item.Group> 
                    
                    </Item.Group>

                {activities.map(activity => (
                <ActivityListItem key={activity.id} activity={activity} />
                ))}     
            </Fragment>
        ))}
        
        </>
        
    )


})