# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

from typing import List

from teams.streaming.payload_transport import PayloadSender
from teams.streaming.payloads import ResponseMessageStream
from teams.streaming.payloads.models import PayloadTypes

from .payload_disassembler import PayloadDisassembler


class ResponseMessageStreamDisassembler(PayloadDisassembler):

    def __init__(self, sender: PayloadSender,
                 content_stream: ResponseMessageStream):
        super().__init__(sender, content_stream.id)

        self.content_stream = content_stream

    @property
    def type(self) -> str:
        return PayloadTypes.STREAM

    async def get_stream(self) -> List[int]:
        # TODO: check if bypass is correct here or if serialization should take place.
        # this is redundant -->stream: List[int] = list(str(self.content_stream.content).encode())

        return self.content_stream.content
