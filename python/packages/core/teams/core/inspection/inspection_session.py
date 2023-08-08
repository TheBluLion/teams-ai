# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

from typing import Any

from teams.core import TurnContext
from teams.schema import Activity, ConversationReference
from teams.connector.aio import ConnectorClient
from teams.connector.auth import MicrosoftAppCredentials


class InspectionSession:

    def __init__(
        self,
        conversation_reference: ConversationReference,
        credentials: MicrosoftAppCredentials,
    ):
        self._conversation_reference = conversation_reference
        self._connector_client = ConnectorClient(
            credentials, base_url=conversation_reference.service_url)

    async def send(self, activity: Activity) -> Any:
        TurnContext.apply_conversation_reference(activity,
                                                 self._conversation_reference)

        try:
            await self._connector_client.conversations.send_to_conversation(
                activity.conversation.id, activity)
        except Exception:
            return False

        return True
